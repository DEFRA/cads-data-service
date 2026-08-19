using Cads.Cds.BuildingBlocks.Core.Domain.Imports;
using System.Linq.Expressions;

namespace Cads.Cds.BuildingBlocks.Application.Imports.Repositories;

public interface IFileImportRepository
{
    IQueryable<FileImport> Set();

    IQueryable<FileImport> Query(bool asNoTracking = true);

    Task<IReadOnlyList<TResult>> ProjectAsync<TResult>(
        Func<IQueryable<FileImport>, IQueryable<TResult>> projection,
        bool asNoTracking = true,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<FileImport>> FindAsync(
       Expression<Func<FileImport, bool>>? filter = null,
       Func<IQueryable<FileImport>, IOrderedQueryable<FileImport>>? orderBy = null,
       string? includeProperties = "",
       bool asNoTracking = true,
       CancellationToken cancellationToken = default);
    
    Task<FileImport?> GetByIdAsync(long id, CancellationToken cancellationToken);

    Task<FileImport?> GetByFileNameAsync(string fileName, CancellationToken cancellationToken);

    Task AddAsync(FileImport entity, CancellationToken cancellationToken);
}