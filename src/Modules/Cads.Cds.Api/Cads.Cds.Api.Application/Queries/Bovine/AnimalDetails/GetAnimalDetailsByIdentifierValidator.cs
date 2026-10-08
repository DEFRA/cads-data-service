using FluentValidation;

namespace Cads.Cds.Api.Application.Queries.Bovine.AnimalDetails;

public class GetAnimalDetailsByIdentifierValidator : AbstractValidator<GetAnimalDetailsByIdentifier>
{
    public GetAnimalDetailsByIdentifierValidator()
    {
        RuleFor(x => x.Identifier.Trim())
          .NotEmpty()
          .MaximumLength(20)
          .Matches("^[A-Za-z0-9 ]+$");
    }
}