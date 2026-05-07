using Cads.Cds.MiBff.Application.Reports.Requests;
using FluentValidation;

namespace Cads.Cds.MiBff.Application.Reports.Validators;

public class GetGbCattleRegistrationsRequestValidator
    : GetReportRequestValidator<GetGbCattleRegistrationsRequest>
{
    public GetGbCattleRegistrationsRequestValidator()
        : base(GetGbCattleRegistrationsRequest.ExpectedKey)
    {
        RuleFor(x => x.Year)
            .InclusiveBetween(1998, DateTime.UtcNow.Year)
            .WithMessage("Year must be between 1998 and the current year.");

        RuleFor(x => x.Month)
            .InclusiveBetween(1, 12)
            .WithMessage("Month must be between 1 and 12.");
    }
}