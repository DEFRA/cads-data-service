using Cads.Cds.BuildingBlocks.Core.Domain.Imports;
using Cads.Cds.BuildingBlocks.Infrastructure.Imports.Repositories;
using Cads.Cds.SystemAdmin.Application.Imports.Repositories;
using Cads.Cds.SystemAdmin.Infrastructure.Persistance.Contexts;

namespace Cads.Cds.SystemAdmin.Infrastructure.Persistance.Repositories;

public class SystemAdminFileImportRepository(
    SystemAdminReadDbContext readDbContext,
    SystemAdminWriteDbContext writeDbContext)
    : ISystemAdminFileImportRepository
{
    private readonly FileImportRepository<SystemAdminReadDbContext, SystemAdminWriteDbContext> _inner = new(readDbContext, writeDbContext);

    public async Task<FileImport?> GetById(long id, CancellationToken cancellationToken)
        => await _inner.GetById(id, cancellationToken);

    public async Task<FileImport?> GetByFileName(string fileName, CancellationToken cancellationToken)
        => await _inner.GetByFileName(fileName, cancellationToken);

    public async Task Add(FileImport entity, CancellationToken cancellationToken)
        => await _inner.Add(entity, cancellationToken);
}