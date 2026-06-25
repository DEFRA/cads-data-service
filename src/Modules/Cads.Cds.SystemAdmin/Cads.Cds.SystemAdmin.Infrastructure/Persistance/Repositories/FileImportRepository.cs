using Cads.Cds.BuildingBlocks.Core.Domain.Imports;
using Cads.Cds.SystemAdmin.Application.Imports.Repositories;
using Cads.Cds.SystemAdmin.Infrastructure.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Cads.Cds.SystemAdmin.Infrastructure.Persistance.Repositories;

public class FileImportRepository(SystemAdminWriteDbContext writeDbContext, SystemAdminReadDbContext readDbContext)
    : IFileImportRepository
{
    private readonly SystemAdminWriteDbContext _writeDbContext = writeDbContext;
    private readonly SystemAdminReadDbContext _readDbContext = readDbContext;

    private IQueryable<FileImport> QueryRead()
        => _readDbContext.FileImports.AsNoTracking();

    public async Task<FileImport?> GetById(long id, CancellationToken cancellationToken)
        => await _writeDbContext.FileImports.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public async Task<FileImport?> GetByFileName(string fileName, CancellationToken cancellationToken)
        => await QueryRead().FirstOrDefaultAsync(x => x.FileName == fileName, cancellationToken);

    public async Task Add(FileImport entity, CancellationToken cancellationToken)
    {
        await _writeDbContext.FileImports.AddAsync(entity, cancellationToken);
    }
}