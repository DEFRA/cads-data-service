using FluentValidation;

namespace Cads.Cds.MiBff.Application.Queries.Reports.Validators;

public abstract class GetReportQueryValidator<T> : AbstractValidator<T>
    where T : GetReportQueryBase
{
    protected GetReportQueryValidator(string expectedKey)
    {
        RuleFor(x => x.ReportKey)
            .NotEmpty()
            .Equal(expectedKey);
    }
}