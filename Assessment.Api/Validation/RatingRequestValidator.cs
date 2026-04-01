using Assessment.Api.Models.Requests;
using FluentValidation;

namespace Assessment.Api.Validation;

/// <summary>
/// Sample validator stub. This one is intentionally minimal — candidates should expand it
/// to cover all business rules (valid business types, valid states, revenue constraints, etc.).
///
/// TODO: Add validation rules for:
///   - Business must be one of the supported types
///   - States must not be empty and must contain only supported values
///   - Revenue must be a positive number
/// </summary>
public class RatingRequestValidator : AbstractValidator<RatingRequest>
{
    public RatingRequestValidator()
    {
        RuleFor(r => r.Business).NotEmpty();
        RuleFor(r => r.Revenue).GreaterThan(0);
        RuleFor(r => r.States).NotEmpty();

        // TODO: Add more specific validation rules
    }
}
