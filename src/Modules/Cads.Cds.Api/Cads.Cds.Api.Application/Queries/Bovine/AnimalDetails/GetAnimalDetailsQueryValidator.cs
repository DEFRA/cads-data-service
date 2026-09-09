using FluentValidation;

namespace Cads.Cds.Api.Application.Queries.Bovine.AnimalDetails;

public class GetAnimalDetailsQueryValidator : AbstractValidator<GetAnimalDetailsQuery>
{
    public GetAnimalDetailsQueryValidator()
    {
        RuleFor(x => x.Identifier).NotEmpty();
    }
}
