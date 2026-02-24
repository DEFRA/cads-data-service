using Cads.Cds.MiBff.Application.Configuration;
using Cads.Cds.MiBff.Core.DTOs;
using FluentValidation;

namespace Cads.Cds.MiBff.Application.Queries.Holdings;

public class GetHoldingsQuery : IPagedQuery<HoldingDTO>
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? Order { get; set; }
    public string? Sort { get; set; }
    public string? Cursor { get; set; }
    public DateTime? LastModified { get; set; }
    public string? Name { get; internal set; }
}

public class GetHoldingsQueryValidator : AbstractValidator<GetHoldingsQuery>
{
    public GetHoldingsQueryValidator(QueryValidationConfig<GetHoldingsQueryValidator> config)
    {
        RuleFor(x => x.Page).GreaterThan(0);
        RuleFor(x => x.PageSize).InclusiveBetween(1, config.MaxPageSize);
        RuleFor(x => x.Sort).Must(s => s == "asc" || s == "desc").When(x => !string.IsNullOrEmpty(x.Sort));
        RuleFor(x => x.Order).NotEmpty().When(x => !string.IsNullOrEmpty(x.Sort));
    }
}