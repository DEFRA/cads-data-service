using Cads.Cds.MiBff.Application.Reports.Requests;

namespace Cads.Cds.MiBff.Application.Reports.Validators;

public class GetHoldingSummaryRequestValidator : GetReportRequestValidator<GetHoldingSummaryRequest>
{
    public GetHoldingSummaryRequestValidator()
        : base(GetHoldingSummaryRequest.ExpectedKey)
    {
    }
}