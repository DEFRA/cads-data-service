using Cads.Cds.BuildingBlocks.Application.Imports.Repositories;
using Cads.Cds.BuildingBlocks.Core.Domain.Imports;
using Cads.Cds.BuildingBlocks.Infrastructure.Database;
using Cads.Cds.BuildingBlocks.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Imports.Repositories;

public abstract class FileImportRepository<TReadContext, TWriteContext>(TReadContext readDbContext, TWriteContext writeDbContext)
    : EFReadWriteRepository<FileImport, TReadContext, TWriteContext>(readDbContext, writeDbContext), IFileImportRepository
    where TReadContext : CadsDbContext
    where TWriteContext : CadsDbContext
{
    public async Task<FileImport?> GetByIdAsync(long id, CancellationToken cancellationToken)
        => await Set().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public async Task<FileImport?> GetByFileNameAsync(string fileName, CancellationToken cancellationToken)
        => await Query().FirstOrDefaultAsync(x => x.FileName.ToUpper() == fileName.ToUpper(), cancellationToken);
}