namespace Assessment.Api.Features.BusinessFactors.Models;

public class CreateBusinessFactorRequest
{
    public string Business { get; set; } = null!;
    public decimal Factor { get; set; }
}
