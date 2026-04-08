namespace Assessment.Api.Features.CarrierEligibility.Models;

public class UpdateCarrierEligibilityRequest
{
    public string Business { get; set; } = null!;
    public string State { get; set; } = null!;
    public bool IsEligible { get; set; }
}
