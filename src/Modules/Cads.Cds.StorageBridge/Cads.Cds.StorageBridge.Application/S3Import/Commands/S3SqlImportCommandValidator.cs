using FluentValidation;

namespace Cads.Cds.StorageBridge.Application.S3Import.Commands;

public class S3SqlImportCommandValidator : AbstractValidator<S3SqlImportCommand>
{
    public S3SqlImportCommandValidator()
    {
        RuleFor(x => x.SourceKey)
            .NotEmpty();
    }
}