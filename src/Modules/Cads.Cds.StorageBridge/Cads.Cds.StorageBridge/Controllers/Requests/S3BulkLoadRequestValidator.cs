using Cads.Cds.StorageBridge.Core.Domain.Enums;
using FluentValidation;

namespace Cads.Cds.StorageBridge.Controllers.Requests;

public class S3BulkLoadRequestValidator : AbstractValidator<S3BulkLoadRequest>
{
    public S3BulkLoadRequestValidator()
    {
        RuleFor(x => x.SourceKey)
            .NotEmpty()
            .WithMessage("SourceKey is required.");

        RuleFor(x => x.BulkImportType)
            .NotEqual(BulkLoadDataTypes.None)
            .WithMessage("BulkImportType must be specified.");

        RuleFor(x => x.Delimiter)
            .Must(d => !char.IsWhiteSpace(d))
            .WithMessage("Delimiter cannot be whitespace.");

        RuleFor(x => x.ActionType)
            .NotEqual(ImportActions.None)
            .WithMessage("ImportActionType cannot be None.");
    }
}