using Assessment.Api.Features.BusinessFactors.Models;
using FluentValidation;

namespace Assessment.Api.Features.BusinessFactors.Validators;

public class CreateBusinessFactorValidator : AbstractValidator<CreateBusinessFactorRequest>
{
    public CreateBusinessFactorValidator()
    {
        RuleFor(x => x.Business)
            .NotEmpty()
            .WithMessage("Business is required")
            .MaximumLength(100)
            .WithMessage("Business name cannot exceed 100 characters");

        RuleFor(x => x.Factor)
            .GreaterThan(0)
            .WithMessage("Factor must be greater than 0")
            .LessThanOrEqualTo(100)
            .WithMessage("Factor cannot exceed 100");
    }
}

public class UpdateBusinessFactorValidator : AbstractValidator<UpdateBusinessFactorRequest>
{
    public UpdateBusinessFactorValidator()
    {
        RuleFor(x => x.Factor)
            .GreaterThan(0)
            .WithMessage("Factor must be greater than 0")
            .LessThanOrEqualTo(100)
            .WithMessage("Factor cannot exceed 100");
    }
}
