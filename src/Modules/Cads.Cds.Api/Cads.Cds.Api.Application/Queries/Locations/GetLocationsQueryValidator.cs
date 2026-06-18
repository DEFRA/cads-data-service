using FluentValidation;

namespace Cads.Cds.Api.Application.Queries.Locations;

public class GetLocationsQueryValidator : AbstractValidator<GetLocationsQuery>
{
    public GetLocationsQueryValidator()
    {
        RuleFor(x => x.Cph)
            .NotEmpty()
            .When(x => x.LastModifiedDate == null);

        RuleFor(x => x.Cph)
            .Matches(@"^[A-Z]{2}-\d{2}/\d{3}/\d{4}$")
            .When(x => x.Cph != null);

        RuleFor(x => x.LastModifiedDate)
            .NotEmpty()
            .When(x => x.Cph == null);
    }
}