using FluentValidation;

namespace Cads.Cds.SystemAdmin.Application.Generation.Commands.CreateGeneration;

public sealed class CreateGenerationCommandValidator
    : AbstractValidator<CreateGenerationCommand>
{
    public CreateGenerationCommandValidator()
    {
        RuleFor(x => x.Table)
            .NotEmpty()
            .WithMessage("Table is required.");
        RuleFor(x => x.Scenario)
            .NotEmpty()
            .WithMessage("Scenario is required.");
        RuleFor(x => x.RowCount)
            .NotNull().WithMessage("Row count is required.")
            .GreaterThan(0).WithMessage("Row count must be greater than zero.");

        When(x => x.BusinessKey.HasValue, () =>
        {
            RuleFor(x => x.BusinessKey)
                .GreaterThan(0)
                .WithMessage("Business key must be greater than zero.");
        });
    }
}