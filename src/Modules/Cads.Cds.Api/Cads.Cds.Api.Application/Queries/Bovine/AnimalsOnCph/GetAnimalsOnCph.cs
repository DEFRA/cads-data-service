using Cads.Cds.Api.Core.Domain.Bovine;
using Cads.Cds.Api.Core.DTOs.Bovine;
using Cads.Cds.BuildingBlocks.Application.Queries;

namespace Cads.Cds.Api.Application.Queries.Bovine.AnimalsOnCph;

public class GetAnimalsOnCph : IQuery<AnimalCollectionDto?>
{
    public const int DefaultPage = 1;
    public const int DefaultPageSize = 25;

    public required string Cph { get; set; }

    public HoldingAssociation HoldingAssociation { get; set; } = HoldingAssociation.MovedOnHolding;

    public IEnumerable<AnimalStatus> Status { get; set; } = [];

    public string? Sex { get; set; }

    public IEnumerable<string> BreedCode { get; set; } = [];

    public DateOnly? DateOnCphFrom { get; set; }

    public string? Q { get; set; }

    public int Page { get; set; } = DefaultPage;

    public int PageSize { get; set; } = DefaultPageSize;

    public AnimalOrderBy OrderBy { get; set; } = AnimalOrderBy.Identifier;

    public SortDirection Direction { get; set; } = SortDirection.Asc;
}