namespace Cads.Cds.SystemAdmin.Application.Imports.Commands;

using FluentValidation;

public sealed class CreateFileImportCommandValidator
    : AbstractValidator<CreateFileImportCommand>
{
    public CreateFileImportCommandValidator()
    {
        RuleFor(x => x.DestinationTableName)
            .NotEmpty()
            .WithMessage("Destination table name is required.");

        RuleFor(x => x.FileName)
            .NotEmpty()
            .WithMessage("File name is required.");

        RuleFor(x => x.TotalRowsToProcess)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Total rows to process must be zero or greater.");

        RuleFor(x => x.RowsFound)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Rows found must be zero or greater.");
    }
}