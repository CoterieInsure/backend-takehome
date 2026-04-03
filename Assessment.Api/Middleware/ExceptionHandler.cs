using System.Net;
using Assessment.Api.Exceptions;

namespace Assessment.Api.Middleware;

/// <summary>
/// Global exception handler middleware.
/// Catches unhandled exceptions and maps them to appropriate HTTP status codes.
///
/// TODO: Add case branches for UnsupportedBusinessTypeException and InvalidStateException
/// (defined in Assessment.Api/Exceptions/) and map them to 400 Bad Request.
/// </summary>
public class ExceptionHandler(RequestDelegate next)
{
    public async Task Invoke(HttpContext httpContext, ILogger<ExceptionHandler> logger)
    {
        try
        {
            await next(httpContext);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Application Error: {Message}", ex.Message);
            await HandleExceptionAsync(ex, httpContext);
        }
    }

    private static async Task HandleExceptionAsync(Exception ex, HttpContext httpContext)
    {
        // TODO: Add cases for UnsupportedBusinessTypeException and InvalidStateException.
        // Both should return 400 Bad Request with { "error": ex.Message }.

        switch (ex)
        {
            case FluentValidation.ValidationException validationException:
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                await httpContext.Response.WriteAsJsonAsync(new
                {
                    errors = validationException.Errors.Select(e => new { e.PropertyName, e.ErrorMessage })
                });
                return;

            case NotImplementedException:
                httpContext.Response.StatusCode = StatusCodes.Status501NotImplemented;
                break;

            default:
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                break;
        }

        await httpContext.Response.WriteAsJsonAsync(new { error = ex.Message });
    }
}
