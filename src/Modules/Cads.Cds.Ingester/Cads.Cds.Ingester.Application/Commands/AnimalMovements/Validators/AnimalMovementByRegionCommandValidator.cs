using FluentValidation;

namespace Cads.Cds.Ingester.Application.Commands.AnimalMovements.Validators;

public class AnimalMovementByRegionCommandValidator : AbstractValidator<AnimalMovementByRegionCommand>
{
    public AnimalMovementByRegionCommandValidator()
    {
        RuleFor(x => x.Region).NotNull();
        RuleFor(x => x.Payload).NotNull();
    }
}