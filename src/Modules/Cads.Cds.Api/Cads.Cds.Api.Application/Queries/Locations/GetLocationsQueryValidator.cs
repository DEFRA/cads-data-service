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

        RuleFor(x => x.Page).GreaterThan(0);
        RuleFor(x => x.PageSize).GreaterThan(0);
        RuleFor(x => x.Sort).Must(s => s == "asc" || s == "desc").When(x => !string.IsNullOrEmpty(x.Sort));
        RuleFor(x => x.Order).NotEmpty().When(x => !string.IsNullOrEmpty(x.Sort));
    }
}