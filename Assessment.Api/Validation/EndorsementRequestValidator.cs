using Assessment.Api.Models.Requests;
using FluentValidation;

namespace Assessment.Api.Validation;

/// <summary>
/// TODO: Implement validation rules for endorsement requests.
/// Consider:
///   - Business must be a supported type
///   - State must be a supported state
///   - Revenue values must be positive
///   - EndorsementEffectiveDate must fall between PolicyStartDate and PolicyEndDate
///   - PolicyEndDate must be after PolicyStartDate
/// </summary>
public class EndorsementRequestValidator : AbstractValidator<EndorsementRequest>
{
    public EndorsementRequestValidator()
    {
        // TODO: Add validation rules
    }
}
