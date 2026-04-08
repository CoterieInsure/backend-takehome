namespace Assessment.Api.Features.CarrierEligibility.Models;

public class CarrierEligibility
{
    public int Id { get; set; }
    public string Business { get; set; } = null!;
    public string State { get; set; } = null!;
    public bool IsEligible { get; set; }
}
