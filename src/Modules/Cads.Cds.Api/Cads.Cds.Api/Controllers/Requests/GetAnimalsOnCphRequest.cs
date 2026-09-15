using Cads.Cds.Api.Application.Queries.Bovine.AnimalsOnCph;
using Cads.Cds.Api.Core.Domain.Bovine;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace Cads.Cds.Api.Controllers.Requests;

public class GetAnimalsOnCphRequest
{
    [FromQuery(Name = "CPH")] public string? Cph { get; set; }

    [FromQuery(Name = "holdingAssociation")]
    [DefaultValue(HoldingAssociation.MovedOnHolding)]
    public HoldingAssociation HoldingAssociation { get; set; }

    [FromQuery(Name = "status")] public AnimalStatus[]? Status { get; set; }

    [FromQuery(Name = "sex")] public AnimalSex? Sex { get; set; }

    [FromQuery(Name = "breedCode")] public string[]? BreedCode { get; set; }

    [FromQuery(Name = "dateOnCPHFrom")] public DateOnly? DateOnCphFrom { get; set; }

    [FromQuery(Name = "q")] public string? Q { get; set; }

    [FromQuery(Name = "page")]
    [DefaultValue(GetAnimalsOnCph.DefaultPage)]
    public int? Page { get; set; }

    [FromQuery(Name = "page-size")]
    [DefaultValue(GetAnimalsOnCph.DefaultPageSize)]
    public int? PageSize { get; set; }

    [FromQuery(Name = "order-by")]
    [DefaultValue(AnimalOrderBy.Identifier)]
    public AnimalOrderBy? OrderBy { get; set; }

    [FromQuery(Name = "direction")]
    [DefaultValue(SortDirection.Asc)]
    public SortDirection? Direction { get; set; }
}