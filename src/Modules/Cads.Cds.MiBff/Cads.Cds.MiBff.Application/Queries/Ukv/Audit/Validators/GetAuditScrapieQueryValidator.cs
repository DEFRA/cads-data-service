using FluentValidation;

namespace Cads.Cds.MiBff.Application.Queries.Ukv.Audit.Validators;

public class GetAuditScrapieQueryValidator : AbstractValidator<GetAuditScrapieQuery>
{
    public GetAuditScrapieQueryValidator()
    {
        RuleFor(x => x.Page).GreaterThan(0);
        RuleFor(x => x.PageSize).GreaterThan(0);
        RuleFor(x => x.Sort).Must(s => s == "asc" || s == "desc").When(x => !string.IsNullOrEmpty(x.Sort));
        RuleFor(x => x.Order).NotEmpty().When(x => !string.IsNullOrEmpty(x.Sort));
    }
}