using FluentValidation;

namespace Cads.Cds.MiBff.Application.Queries.Reports.Vaidators;

public class GetUserReportsByEmailQueryValidator : AbstractValidator<GetUserReportsByEmailQuery>
{
    public GetUserReportsByEmailQueryValidator()
    {
        RuleFor(x => x.Email).NotEmpty();
    }
}