using Cads.Cds.MiBff.Application.Reports.Requests;
using FluentValidation;

namespace Cads.Cds.MiBff.Application.Reports.Validators;

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
