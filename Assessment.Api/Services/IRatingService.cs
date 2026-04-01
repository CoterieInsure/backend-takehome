using Assessment.Api.Models.Requests;
using Assessment.Api.Models.Responses;

namespace Assessment.Api.Services;

/// <summary>
/// Calculates insurance premiums based on business type, revenue, and state.
/// TODO: Implement this interface.
/// </summary>
public interface IRatingService
{
    /// <summary>
    /// Calculate premiums for the given business across the requested states.
    /// </summary>
    Task<RatingResponse> CalculatePremiumAsync(RatingRequest request);

    /// <summary>
    /// Process a mid-term endorsement and calculate the pro-rata premium adjustment.
    /// </summary>
    Task<EndorsementResponse> ProcessEndorsementAsync(EndorsementRequest request);
}
