using Cads.Cds.Api.Core.Domain.Entities.Animals;

namespace Cads.Cds.Api.Application.Queries.Bovine.AnimalsOnHolding;

public sealed record AnimalsOnHoldingResult(
    IReadOnlyList<AnimalOnHolding> Items,
    long TotalCount,
    int Page,
    int PageSize);
