using FluentValidation;

namespace Cads.Cds.SystemAdmin.Application.Imports.Queries.GetFileImportByFileName;

public sealed class GetFileImportByFileNameQueryValidator : AbstractValidator<GetFileImportByFileNameQuery>
{
    public GetFileImportByFileNameQueryValidator()
    {
        RuleFor(x => x.FileName)
            .NotEmpty()
            .WithMessage("File name must be provided.");
    }
}