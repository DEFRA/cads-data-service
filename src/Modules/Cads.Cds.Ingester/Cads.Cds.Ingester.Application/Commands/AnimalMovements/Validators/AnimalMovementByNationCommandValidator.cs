using FluentValidation;

namespace Cads.Cds.Ingester.Application.Commands.AnimalMovements.Validators;

public class AnimalMovementByNationCommandValidator : AbstractValidator<AnimalMovementByNationCommand>
{
    public AnimalMovementByNationCommandValidator()
    {
        RuleFor(x => x.Nation).NotNull();
        RuleFor(x => x.Payload).NotNull();
    }
}