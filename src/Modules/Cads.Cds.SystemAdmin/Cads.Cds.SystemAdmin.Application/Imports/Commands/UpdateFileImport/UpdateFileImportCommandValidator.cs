using FluentValidation;

namespace Cads.Cds.SystemAdmin.Application.Imports.Commands.UpdateFileImport;

public sealed class UpdateFileImportCommandValidator
    : AbstractValidator<UpdateFileImportCommand>
{
    public UpdateFileImportCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("A valid Id must be provided.");

        RuleFor(x => x.TotalRowsToProcess)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Total rows to process must be zero or greater.");

        RuleFor(x => x.RowsFound)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Rows found must be zero or greater.");

        RuleFor(x => x.ImportStatus)
            .IsInEnum()
            .WithMessage("ImportStatus must be a valid state.");
    }
}
