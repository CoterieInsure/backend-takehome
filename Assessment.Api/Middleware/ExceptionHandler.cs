using System.Net;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace Assessment.Api.Middleware;

/// <summary>
/// Global exception handler middleware.
/// Catches unhandled exceptions and maps them to appropriate HTTP status codes.
///
/// TODO: Add your own custom exception types (e.g., UnsupportedBusinessTypeException,
/// InvalidStateException) and map them to the correct HTTP status codes here.
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
        // TODO: Add cases for your custom exception types.
        // For example:
        //   case UnsupportedBusinessTypeException:
        //       httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        //       break;

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
