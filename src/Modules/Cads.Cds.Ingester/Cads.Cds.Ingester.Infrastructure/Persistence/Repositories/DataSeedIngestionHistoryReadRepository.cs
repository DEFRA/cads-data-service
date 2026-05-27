using Cads.Cds.BuildingBlocks.Infrastructure.Persistence.Repositories;
using Cads.Cds.Ingester.Core.Domain.Entities;
using Cads.Cds.Ingester.Core.Domain.Repositories;
using Cads.Cds.Ingester.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Cads.Cds.Ingester.Infrastructure.Persistence.Repositories;

public class DataSeedIngestionHistoryReadRepository(IngesterReadDbContext dbContext)
    : EFReadOnlyRepository<DataSeedIngestionHistory, IngesterReadDbContext>(dbContext),
        IDataSeedIngestionHistoryReadRepository
{
    public async Task<DataSeedIngestionHistory?> GetByIdAsync(
        long id,
        CancellationToken cancellationToken = default)
    {
        return await Query().SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<DataSeedIngestionHistory?> GetByFileNameAsync(
        string fileName,
        CancellationToken cancellationToken = default)
    {
        return await Query().SingleOrDefaultAsync(x => x.FileName == fileName, cancellationToken);
    }
}