using Assessment.Api.Models.Requests;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Assessment.Api;

public static class EndpointMappings
{
    public static void MapEndpoints(this WebApplication app)
    {
        app.MapGet("/healthz", () => Results.Ok("healthy"));

        var v1 = app.MapGroup("/api/v1");

        var rating = v1.MapGroup("/rating");
        rating.MapPost("/", CalculateRating);
    }

    /// <summary>
    /// TODO: Implement this endpoint.
    /// Accept a RatingRequest, validate it, calculate premiums for each state, and return a RatingResponse.
    /// </summary>
    private static async Task<IResult> CalculateRating(
        RatingRequest request,
        [FromServices] IValidator<RatingRequest> validator
        // TODO: Inject your rating service here
        )
    {
        await validator.ValidateAndThrowAsync(request);

        // TODO: Call your rating service to calculate premiums
        // return Results.Ok(response);

        throw new NotImplementedException("Implement the rating calculation");
    }
}
