using FluentValidation;

namespace Cads.Cds.SystemAdmin.Application.Imports.Commands.BatchUpdateFileImport;

public sealed class BatchUpdateFileImportCommandValidator
    : AbstractValidator<BatchUpdateFileImportCommand>
{
    public BatchUpdateFileImportCommandValidator()
    {
        RuleFor(x => x.GroupKey)
            .NotEmpty()
            .WithMessage("A valid group key must be provided.");

        RuleFor(x => x.TotalRowsToProcess)
            .GreaterThanOrEqualTo(0).When(x => x.TotalRowsToProcess.HasValue)
            .WithMessage("Total rows to process must be zero or greater.");

        RuleFor(x => x.RowsFound)
            .GreaterThanOrEqualTo(0).When(x => x.RowsFound.HasValue)
            .WithMessage("Rows found must be zero or greater.");

        RuleFor(x => x.ImportStatus)
            .IsInEnum().When(x => x.ImportStatus.HasValue)
            .WithMessage("ImportStatus must be a valid state.");
    }
}