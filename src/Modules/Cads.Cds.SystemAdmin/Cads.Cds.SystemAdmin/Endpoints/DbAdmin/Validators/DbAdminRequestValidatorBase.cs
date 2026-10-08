using Cads.Cds.SystemAdmin.Endpoints.DbAdmin.Requests;
using FluentValidation;

namespace Cads.Cds.SystemAdmin.Endpoints.DbAdmin.Validators;

public abstract class DbAdminRequestValidatorBase<T> : AbstractValidator<T>
    where T : DbAdminRequestBase
{
    protected DbAdminRequestValidatorBase(IEnumerable<string> allowedCommands)
    {
        RuleFor(x => x.Command)
            .NotEmpty()
            .WithMessage("Command is required.");

        RuleFor(x => x.Command)
            .Must(command => allowedCommands.ToArray().Contains(command))
            .WithMessage("Command is not recognised.");
    }
}