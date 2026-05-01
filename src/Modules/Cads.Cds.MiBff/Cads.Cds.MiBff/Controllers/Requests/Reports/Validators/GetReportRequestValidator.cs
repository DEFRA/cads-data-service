using FluentValidation;

namespace Cads.Cds.MiBff.Controllers.Requests.Reports.Validators;

public abstract class GetReportRequestValidator<T> : AbstractValidator<T>
    where T : GetReportRequest
{
    protected GetReportRequestValidator(string expectedKey)
    {
        RuleFor(x => x.ReportKey)
            .NotEmpty()
            .Equal(expectedKey);
    }
}
