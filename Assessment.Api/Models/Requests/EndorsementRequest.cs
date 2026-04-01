namespace Assessment.Api.Models.Requests;

/// <summary>
/// Request payload for processing a mid-term policy endorsement (coverage change).
/// An endorsement modifies an existing policy and results in a pro-rata premium adjustment.
/// </summary>
public class EndorsementRequest
{
    /// <summary>
    /// The type of business on the original policy.
    /// </summary>
    public string Business { get; set; } = string.Empty;

    /// <summary>
    /// The original annual revenue used when the policy was bound.
    /// </summary>
    public decimal OriginalRevenue { get; set; }

    /// <summary>
    /// The updated annual revenue after the endorsement change.
    /// For example, the insured's revenue projection changed mid-term.
    /// </summary>
    public decimal NewRevenue { get; set; }

    /// <summary>
    /// The state where the policy is active (abbreviation or full name).
    /// </summary>
    public string State { get; set; } = string.Empty;

    /// <summary>
    /// The date the original policy term started (inception date).
    /// </summary>
    public DateTime PolicyStartDate { get; set; }

    /// <summary>
    /// The date the original policy term ends (expiration date). Typically one year after start.
    /// </summary>
    public DateTime PolicyEndDate { get; set; }

    /// <summary>
    /// The effective date of this endorsement (when the coverage change takes effect).
    /// Must be between PolicyStartDate and PolicyEndDate.
    /// </summary>
    public DateTime EndorsementEffectiveDate { get; set; }
}
