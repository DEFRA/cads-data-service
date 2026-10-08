using Cads.Cds.Api.Core.Domain.Entities.Animals;

namespace Cads.Cds.Api.Application.Queries.Bovine.AnimalDetails;

public interface IAnimalDetailReadQuery
{
    Task<AnimalDetail?> GetByIdentifierAsync(string identifier, CancellationToken cancellationToken = default);
}