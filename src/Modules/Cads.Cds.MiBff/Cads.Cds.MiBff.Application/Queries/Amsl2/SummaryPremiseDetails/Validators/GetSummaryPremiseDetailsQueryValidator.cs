using Cads.Cds.MiBff.Application.Queries.Amsl2.SummaryPremiseDetails;
using FluentValidation;

namespace Cads.Cds.MiBff.Application.Queries.Ukv.Animals.Validators;

public class GetSummaryPremiseDetailsQueryValidator : AbstractValidator<GetSummaryPremiseDetailsQuery>
{
    public GetSummaryPremiseDetailsQueryValidator()
    {
        RuleFor(x => x.Page).GreaterThan(0);
        RuleFor(x => x.PageSize).GreaterThan(0);
        RuleFor(x => x.Sort).Must(s => s == "asc" || s == "desc").When(x => !string.IsNullOrEmpty(x.Sort));
        RuleFor(x => x.Order).NotEmpty().When(x => !string.IsNullOrEmpty(x.Sort));
    }
}