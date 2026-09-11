using FluentValidation;

namespace Cads.Cds.Api.Application.Queries.Bovine.AnimalsOnCph;

public class GetAnimalsOnCphValidator : AbstractValidator<GetAnimalsOnCph>
{
    public GetAnimalsOnCphValidator()
    {
        RuleFor(x => x.Cph).NotEmpty();

        RuleFor(x => x.Page).GreaterThanOrEqualTo(1);

        RuleFor(x => x.PageSize).GreaterThanOrEqualTo(1);
    }
}
