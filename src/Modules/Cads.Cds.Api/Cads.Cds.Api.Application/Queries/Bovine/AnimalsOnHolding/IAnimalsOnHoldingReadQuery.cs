namespace Cads.Cds.Api.Application.Queries.Bovine.AnimalsOnHolding;

public interface IAnimalsOnHoldingReadQuery
{
    Task<AnimalsOnHoldingResult> ExecuteAsync(
        GetAnimalsOnHolding query,
        CancellationToken cancellationToken = default);
}
