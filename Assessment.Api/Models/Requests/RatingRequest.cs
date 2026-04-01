namespace Assessment.Api.Models.Requests;

/// <summary>
/// Request payload for calculating an insurance premium quote.
/// </summary>
public class RatingRequest
{
    /// <summary>
    /// The type of business being insured (e.g., "Plumber", "Architect", "Programmer").
    /// </summary>
    public string Business { get; set; } = string.Empty;

    /// <summary>
    /// The annual revenue of the business in dollars. Used as the rating basis for premium calculation.
    /// </summary>
    public decimal Revenue { get; set; }

    /// <summary>
    /// One or more states where coverage is requested. Accepts full state names or abbreviations (e.g., "TX" or "Texas").
    /// </summary>
    public string[] States { get; set; } = [];
}
