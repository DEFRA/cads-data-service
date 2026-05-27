using Cads.Cds.StorageBridge.Core.Domain.Enums;
using FluentValidation;

namespace Cads.Cds.StorageBridge.Application.BulkLoad.Commands;

public class S3BulkLoadCommandValidator : AbstractValidator<S3BulkLoadCommand>
{
    public S3BulkLoadCommandValidator()
    {
        RuleFor(x => x.SourceKey)
            .NotEmpty();

        RuleFor(x => x.BulkImportType)
            .NotEqual(BulkLoadDataType.None);

        RuleFor(x => x.ActionType)
            .NotEqual(ImportActions.None);

        RuleFor(x => x.Delimiter)
            .Must(d => !char.IsWhiteSpace(d));
    }
}