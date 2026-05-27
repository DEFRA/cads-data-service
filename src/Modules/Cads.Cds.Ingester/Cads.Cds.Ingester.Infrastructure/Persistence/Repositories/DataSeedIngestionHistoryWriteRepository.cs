using Cads.Cds.BuildingBlocks.Infrastructure.Persistence.Repositories;
using Cads.Cds.Ingester.Core.Domain.Entities;
using Cads.Cds.Ingester.Core.Domain.Repositories;
using Cads.Cds.Ingester.Infrastructure.Persistence.Contexts;

namespace Cads.Cds.Ingester.Infrastructure.Persistence.Repositories;

public class DataSeedIngestionHistoryWriteRepository(IngesterWriteDbContext dbContext)
    : EFRepository<DataSeedIngestionHistory, IngesterWriteDbContext>(dbContext),
        IDataSeedIngestionHistoryWriteRepository
{
}