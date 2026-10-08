using Cads.Cds.Api.Application.Queries.Bovine.AnimalDetails;
using Cads.Cds.Api.Core.Domain.Entities.Animals;
using Cads.Cds.Api.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Cads.Cds.Api.Infrastructure.Persistence.Queries.Animals;

public class AnimalDetailReadQuery(ApiReadDbContext dbContext) : IAnimalDetailReadQuery
{
    public async Task<AnimalDetail?> GetByIdentifierAsync(
        string identifier,
        CancellationToken cancellationToken = default)
    {
        return await dbContext
            .GetAnimalDetail(identifier)
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken);
    }
}