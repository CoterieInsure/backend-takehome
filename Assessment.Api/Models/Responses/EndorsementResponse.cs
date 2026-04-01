namespace Assessment.Api.Models.Responses;

/// <summary>
/// Response payload for a processed endorsement, showing the pro-rata premium adjustment.
/// </summary>
public class EndorsementResponse
{
    public string Business { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;

    /// <summary>
    /// The original annual premium before the endorsement.
    /// </summary>
    public decimal OriginalPremium { get; set; }

    /// <summary>
    /// The new annual premium after the endorsement change.
    /// </summary>
    public decimal NewPremium { get; set; }

    /// <summary>
    /// The pro-rata premium adjustment for the remaining policy term.
    /// Positive = additional premium due; Negative = return premium.
    /// Calculated as: (NewPremium - OriginalPremium) * (DaysRemaining / TotalDays)
    /// </summary>
    public decimal ProRataAdjustment { get; set; }

    /// <summary>
    /// The effective date of the endorsement.
    /// </summary>
    public DateTime EffectiveDate { get; set; }
}
