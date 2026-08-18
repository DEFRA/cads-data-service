using FluentValidation;

namespace Cads.Cds.StorageBridge.Application.S3Import.Commands;

public class S3CsvImportCommandValidator : AbstractValidator<S3CsvImportCommand>
{
    public S3CsvImportCommandValidator()
    {
        RuleFor(x => x.SourceKey)
            .NotEmpty()
            .When(x => x.FileImportId == null);

        RuleFor(x => x.FileImportId)
            .NotNull()
            .GreaterThan(0)
            .WithMessage("'File Import Id' must not be null and be greater than zero.")
            .When(x => x.SourceKey == null);

        RuleFor(x => x.Delimiter)
            .Must(d => !char.IsWhiteSpace(d));
    }
}