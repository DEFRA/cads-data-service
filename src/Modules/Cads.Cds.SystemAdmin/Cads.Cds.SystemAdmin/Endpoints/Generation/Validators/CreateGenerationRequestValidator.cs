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

        RuleFor(x => x)
            .Must(r => (r.BusinessKeys is not null) ^ r.RowCount.HasValue)
            .WithMessage("Exactly one of Business keys or Row count must be provided.");

        RuleFor(x => x.RowCount)
            .GreaterThan(0)
            .When(x => x.RowCount.HasValue)
            .WithMessage("Row count must be greater than zero.");

        RuleFor(x => x.BusinessKeys)
            .NotEmpty()
            .When(x => x.BusinessKeys is not null)
            .WithMessage("Business keys are required.");
    }
}