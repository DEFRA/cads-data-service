using FluentValidation;

namespace Cads.Cds.Api.Application.Queries.Bovine.AnimalsOnHolding;

public class GetAnimalsOnHoldingValidator : AbstractValidator<GetAnimalsOnHolding>
{
    public GetAnimalsOnHoldingValidator()
    {
        RuleFor(x => x.Cph).NotEmpty();
    }
}