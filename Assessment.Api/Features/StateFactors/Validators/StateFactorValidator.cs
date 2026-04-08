using Assessment.Api.Features.StateFactors.Models;
using FluentValidation;

namespace Assessment.Api.Features.StateFactors.Validators;

public class CreateStateFactorValidator : AbstractValidator<CreateStateFactorRequest>
{
    public CreateStateFactorValidator()
    {
        RuleFor(x => x.State)
            .NotEmpty()
            .WithMessage("State is required")
            .MaximumLength(50)
            .WithMessage("State name cannot exceed 50 characters");

        RuleFor(x => x.Factor)
            .GreaterThan(0)
            .WithMessage("Factor must be greater than 0")
            .LessThanOrEqualTo(100)
            .WithMessage("Factor cannot exceed 100");
    }
}

public class UpdateStateFactorValidator : AbstractValidator<UpdateStateFactorRequest>
{
    public UpdateStateFactorValidator()
    {
        RuleFor(x => x.Factor)
            .GreaterThan(0)
            .WithMessage("Factor must be greater than 0")
            .LessThanOrEqualTo(100)
            .WithMessage("Factor cannot exceed 100");
    }
}
