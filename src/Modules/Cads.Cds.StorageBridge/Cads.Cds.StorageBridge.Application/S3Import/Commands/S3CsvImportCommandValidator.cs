using FluentValidation;

namespace Cads.Cds.StorageBridge.Application.S3Import.Commands;

public class S3CsvImportCommandValidator : AbstractValidator<S3CsvImportCommand>
{
    public S3CsvImportCommandValidator()
    {
        RuleFor(x => x.SourceKey)
            .NotEmpty();

        RuleFor(x => x.Delimiter)
            .Must(d => !char.IsWhiteSpace(d));
    }
}