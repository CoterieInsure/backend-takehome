namespace Assessment.Api.Features.StateFactors.Models;

public class CreateStateFactorRequest
{
    public string State { get; set; } = null!;
    public decimal Factor { get; set; }
}
