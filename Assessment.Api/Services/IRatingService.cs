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
    /// Must call ICarrierApiClient.ValidateEligibilityAsync for each state to confirm
    /// the carrier will write the business in that state before calculating.
    /// </summary>
    Task<RatingResponse> CalculatePremiumAsync(RatingRequest request);
}
