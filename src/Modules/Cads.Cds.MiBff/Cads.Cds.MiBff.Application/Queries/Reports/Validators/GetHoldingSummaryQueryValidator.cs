namespace Cads.Cds.MiBff.Application.Queries.Reports.Validators;

public class GetHoldingSummaryQueryValidator : GetReportQueryValidator<GetHoldingSummaryQuery>
{
    public GetHoldingSummaryQueryValidator()
        : base(GetHoldingSummaryQuery.ExpectedKey)
    {
    }
}