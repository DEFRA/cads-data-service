using Cads.Cds.BuildingBlocks.Core.Domain.Imports;
using Cads.Cds.BuildingBlocks.Infrastructure.Imports.Repositories;
using Cads.Cds.StorageBridge.Application.Imports.Repositories;
using Cads.Cds.StorageBridge.Infrastructure.Persistance.Contexts;

namespace Cads.Cds.StorageBridge.Infrastructure.Persistance.Repositories;

public class StorageBridgeFileImportRepository(
    StorageBridgeReadDbContext readDbContext,
    StorageBridgeWriteDbContext writeDbContext)
    : IStorageBridgeFileImportRepository
{
    private readonly FileImportRepository<StorageBridgeReadDbContext, StorageBridgeWriteDbContext> _inner = new(readDbContext, writeDbContext);

    public async Task<FileImport?> GetById(long id, CancellationToken cancellationToken)
        => await _inner.GetById(id, cancellationToken);

    public async Task<FileImport?> GetByFileName(string fileName, CancellationToken cancellationToken)
        => await _inner.GetByFileName(fileName, cancellationToken);

    public async Task Add(FileImport entity, CancellationToken cancellationToken)
        => await _inner.Add(entity, cancellationToken);
}