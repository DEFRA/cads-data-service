using FluentValidation;

namespace Cads.Cds.MiBff.Application.Queries.Reports.Validators;

public class GetGbCattleRegistrationsQueryValidator : GetReportQueryValidator<GetGbCattleRegistrationsQuery>
{
    public GetGbCattleRegistrationsQueryValidator()
        : base(GetGbCattleRegistrationsQuery.ExpectedKey)
    {
        RuleFor(x => x.Year)
            .InclusiveBetween(1998, DateTime.UtcNow.Year);

        RuleFor(x => x.Month)
            .InclusiveBetween(1, 12);
    }
}