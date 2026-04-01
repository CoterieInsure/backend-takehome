using Assessment.Api.Models.Requests;
using Assessment.Api.Models.Responses;
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

        var endorsements = v1.MapGroup("/endorsements");
        endorsements.MapPost("/", ProcessEndorsement);
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

    /// <summary>
    /// TODO: Implement this endpoint.
    /// Accept an EndorsementRequest, validate it, calculate the pro-rata premium adjustment, and return an EndorsementResponse.
    /// </summary>
    private static async Task<IResult> ProcessEndorsement(
        EndorsementRequest request,
        [FromServices] IValidator<EndorsementRequest> validator
        // TODO: Inject your endorsement/rating service here
        )
    {
        await validator.ValidateAndThrowAsync(request);

        // TODO: Call your service to process the endorsement
        // return Results.Ok(response);

        throw new NotImplementedException("Implement the endorsement processing");
    }
}
