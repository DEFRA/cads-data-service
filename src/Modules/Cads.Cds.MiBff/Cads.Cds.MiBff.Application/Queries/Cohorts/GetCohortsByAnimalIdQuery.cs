using Cads.Cds.BuildingBlocks.Application.Queries;
using Cads.Cds.MiBff.Core.DTOs;
using FluentValidation;

namespace Cads.Cds.MiBff.Application.Queries.Cohorts;

public class GetCohortsByAnimalIdQuery : IPagedQuery<UkvDto>
{
    public Guid AnimalId { get; set; }

    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? Order { get; set; }
    public string? Sort { get; set; }
}

public class GetCohortsByAnimalIdQueryValidator : AbstractValidator<GetCohortsByAnimalIdQuery>
{
    public GetCohortsByAnimalIdQueryValidator()
    {
        RuleFor(x => x.AnimalId).NotEmpty();
        RuleFor(x => x.Page).GreaterThan(0);
        RuleFor(x => x.PageSize).GreaterThan(0);
        RuleFor(x => x.Sort).Must(s => s == "asc" || s == "desc").When(x => !string.IsNullOrEmpty(x.Sort));
        RuleFor(x => x.Order).NotEmpty().When(x => !string.IsNullOrEmpty(x.Sort));
    }
}