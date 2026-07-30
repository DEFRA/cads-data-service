using Cads.Cds.BuildingBlocks.Application.Imports.Repositories;
using Cads.Cds.BuildingBlocks.Core.Domain.Imports;
using Cads.Cds.BuildingBlocks.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Imports.Repositories;

public class FileImportRepository<TReadContext, TWriteContext>(TReadContext readDbContext, TWriteContext writeDbContext)
    : IFileImportRepository
    where TReadContext : CadsDbContext
    where TWriteContext : CadsDbContext
{
    private readonly TReadContext _readDbContext = readDbContext;
    private readonly TWriteContext _writeDbContext = writeDbContext;

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