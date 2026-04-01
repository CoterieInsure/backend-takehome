namespace Assessment.Api.Models.Responses;

/// <summary>
/// Response payload containing calculated premiums for each requested state.
/// </summary>
public class RatingResponse
{
    public string Business { get; set; } = string.Empty;
    public decimal Revenue { get; set; }
    public List<StatePremium> Premiums { get; set; } = [];
}

/// <summary>
/// The calculated premium for a single state.
/// </summary>
public class StatePremium
{
    public decimal Premium { get; set; }
    public string State { get; set; } = string.Empty;
}
