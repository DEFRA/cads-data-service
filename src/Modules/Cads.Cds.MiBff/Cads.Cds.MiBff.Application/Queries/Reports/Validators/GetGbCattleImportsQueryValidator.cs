using FluentValidation;

namespace Cads.Cds.MiBff.Application.Queries.Reports.Validators;

public class GetGbCattleImportsQueryValidator : GetReportQueryValidator<GetGbCattleImportsQuery>
{
    public GetGbCattleImportsQueryValidator()
        : base(GetGbCattleImportsQuery.ExpectedKey)
    {
        RuleFor(x => x.Year)
            .InclusiveBetween(1998, DateTime.UtcNow.Year);
    }
}