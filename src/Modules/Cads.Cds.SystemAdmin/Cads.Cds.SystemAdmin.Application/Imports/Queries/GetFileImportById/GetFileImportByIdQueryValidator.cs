using FluentValidation;

namespace Cads.Cds.SystemAdmin.Application.Imports.Queries.GetFileImportById;

public sealed class GetFileImportByIdQueryValidator : AbstractValidator<GetFileImportByIdQuery>
{
    public GetFileImportByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("File ID must be provided.");
    }
}