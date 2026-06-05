using FluentValidation;

namespace Cads.Cds.Api.Application.Queries.Locations;

public class GetLocationsQueryValidator : AbstractValidator<GetLocationsQuery>
{
    public GetLocationsQueryValidator()
    {
        RuleFor(x => x.Cph)
            .NotEmpty()
            .When(x => x.LastModifiedDate == null);

        RuleFor(x => x.LastModifiedDate)
            .NotEmpty()
            .When(x => x.Cph == null);
    }
}