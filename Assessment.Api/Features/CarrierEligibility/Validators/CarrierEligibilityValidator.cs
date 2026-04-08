using Assessment.Api.Features.CarrierEligibility.Models;
using Assessment.Api.Features.BusinessFactors.Services;
using Assessment.Api.Features.StateFactors.Services;
using FluentValidation;

namespace Assessment.Api.Features.CarrierEligibility.Validators;

public class CreateCarrierEligibilityValidator : AbstractValidator<CreateCarrierEligibilityRequest>
{
    public CreateCarrierEligibilityValidator(
        IBusinessFactorService businessFactorService,
        IStateFactorService stateFactorService)
    {
        RuleFor(x => x.Business)
            .NotEmpty()
            .WithMessage("Business is required")
            .MaximumLength(100)
            .WithMessage("Business name cannot exceed 100 characters")
            .MustAsync(async (business, cancellation) =>
                await businessFactorService.ExistsAsync(business))
            .WithMessage((model, business) => $"Business '{business}' does not exist");

        RuleFor(x => x.State)
            .NotEmpty()
            .WithMessage("State is required")
            .MaximumLength(50)
            .WithMessage("State name cannot exceed 50 characters")
            .MustAsync(async (state, cancellation) =>
                await stateFactorService.ExistsAsync(state))
            .WithMessage((model, state) => $"State '{state}' does not exist");
    }
}

public class UpdateCarrierEligibilityValidator : AbstractValidator<UpdateCarrierEligibilityRequest>
{
    public UpdateCarrierEligibilityValidator(
        IBusinessFactorService businessFactorService,
        IStateFactorService stateFactorService)
    {
        RuleFor(x => x.Business)
            .NotEmpty()
            .WithMessage("Business is required")
            .MaximumLength(100)
            .WithMessage("Business name cannot exceed 100 characters")
            .MustAsync(async (business, cancellation) =>
                await businessFactorService.ExistsAsync(business))
            .WithMessage((model, business) => $"Business '{business}' does not exist");

        RuleFor(x => x.State)
            .NotEmpty()
            .WithMessage("State is required")
            .MaximumLength(50)
            .WithMessage("State name cannot exceed 50 characters")
            .MustAsync(async (state, cancellation) =>
                await stateFactorService.ExistsAsync(state))
            .WithMessage((model, state) => $"State '{state}' does not exist");
    }
}
