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
         .Must(r => (r.BusinessKeys?.Any()).GetValueOrDefault() || r.RowCount.GetValueOrDefault() > 0)
         .WithMessage("Business keys or a Row count greater than zero is required.");
    }
}