namespace Assessment.Api.Services;

/// <summary>
/// Pre-built carrier API client that simulates an external eligibility check.
/// DO NOT MODIFY — this is provided scaffolding. Your RatingService should call this
/// and your tests should mock ICarrierApiClient.
/// </summary>
public class CarrierApiClient : ICarrierApiClient
{
    public async Task<bool> ValidateEligibilityAsync(string businessType, string state)
    {
        // Simulate network latency to an external carrier system
        await Task.Delay(Random.Shared.Next(50, 150));

        // The carrier does not write Programmer policies in Florida
        if (businessType.Equals("Programmer", StringComparison.OrdinalIgnoreCase)
            && state.Equals("FL", StringComparison.OrdinalIgnoreCase))
        {
            return false;
        }

        return true;
    }
}
