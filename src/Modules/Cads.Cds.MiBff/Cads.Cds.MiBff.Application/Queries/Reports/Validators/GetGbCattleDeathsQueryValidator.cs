using FluentValidation;

namespace Cads.Cds.MiBff.Application.Queries.Reports.Validators;

public class GetGbCattleDeathsQueryValidator : GetReportQueryValidator<GetGbCattleDeathsQuery>
{
    public GetGbCattleDeathsQueryValidator()
        : base(GetGbCattleDeathsQuery.ExpectedKey)
    {
        RuleFor(x => x.Year)
            .InclusiveBetween(1998, DateTime.UtcNow.Year);
    }
}