using Cads.Cds.Api.Core.Domain.Bovine;
using Microsoft.AspNetCore.Mvc;

namespace Cads.Cds.Api.Controllers.Requests;

public class GetAnimalsOnCphRequest
{
    [FromQuery(Name = "CPH")] public string? Cph { get; set; }

    [FromQuery(Name = "holdingAssociation")] public HoldingAssociation? HoldingAssociation { get; set; }

    [FromQuery(Name = "status")] public AnimalStatus[]? Status { get; set; }

    [FromQuery(Name = "sex")] public string? Sex { get; set; }

    [FromQuery(Name = "breedCode")] public string[]? BreedCode { get; set; }

    [FromQuery(Name = "dateOnCPHFrom")] public DateOnly? DateOnCphFrom { get; set; }

    [FromQuery(Name = "q")] public string? Q { get; set; }

    [FromQuery(Name = "page")] public int? Page { get; set; }

    [FromQuery(Name = "page-size")] public int? PageSize { get; set; }

    [FromQuery(Name = "order-by")] public AnimalOrderBy? OrderBy { get; set; }

    [FromQuery(Name = "direction")] public SortDirection? Direction { get; set; }
}