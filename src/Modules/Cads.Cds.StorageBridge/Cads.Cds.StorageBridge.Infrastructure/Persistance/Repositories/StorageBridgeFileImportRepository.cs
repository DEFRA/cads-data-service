using Cads.Cds.BuildingBlocks.Infrastructure.Imports.Repositories;
using Cads.Cds.StorageBridge.Application.Imports.Repositories;
using Cads.Cds.StorageBridge.Infrastructure.Persistance.Contexts;

namespace Cads.Cds.StorageBridge.Infrastructure.Persistance.Repositories;

public class StorageBridgeFileImportRepository(
    StorageBridgeReadDbContext readDbContext,
    StorageBridgeWriteDbContext writeDbContext)
    : FileImportRepository<StorageBridgeReadDbContext, StorageBridgeWriteDbContext>(readDbContext, writeDbContext), IStorageBridgeFileImportRepository
{
}