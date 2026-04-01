namespace Assessment.Api.Services;

/// <summary>
/// Simulates an external carrier API that must be called during rating.
/// In production, this would call the carrier's policy administration system to validate
/// that the business classification is eligible for coverage.
///
/// TODO: Implement this interface. Your implementation should simulate an async external call
/// (e.g., using Task.Delay) and return whether the business type is eligible in the given state.
/// </summary>
public interface ICarrierApiClient
{
    /// <summary>
    /// Validates with the carrier that the given business type can be written in the specified state.
    /// </summary>
    /// <param name="businessType">The business classification (e.g., "Plumber").</param>
    /// <param name="state">The two-letter state abbreviation (e.g., "TX").</param>
    /// <returns>True if the carrier will write this business in this state.</returns>
    Task<bool> ValidateEligibilityAsync(string businessType, string state);
}
