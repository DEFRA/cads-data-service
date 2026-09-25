using Cads.Cds.SystemAdmin.Endpoints.Generation.Requests;
using FluentValidation;

namespace Cads.Cds.SystemAdmin.Endpoints.Generation.Validators;

public sealed class CreateGenerationRequestValidator
    : AbstractValidator<CreateGenerationRequest>
{
    public CreateGenerationRequestValidator()
    {
        RuleFor(x => x.Scenario)
            .NotEmpty()
            .WithMessage("Scenario is required.");
        RuleFor(x => x.RowCount)
            .NotNull().WithMessage("Row count is required.")
            .GreaterThan(0).WithMessage("Row count must be greater than zero.");
    }
}