using FluentValidation;

namespace Cads.Cds.BuildingBlocks.Application.Commands.Validators;

public abstract class IdOnlyCommandValidator<T> : AbstractValidator<T>
    where T : IHasId
{
    protected IdOnlyCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("A valid Id must be provided.");
    }
}