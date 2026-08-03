using Cads.Cds.BuildingBlocks.Core.Domain.Imports;

namespace Cads.Cds.BuildingBlocks.Application.Imports.Repositories;

public interface IFileImportRepository
{
    Task<FileImport?> GetByIdAsync(long id, CancellationToken cancellationToken);
    Task<FileImport?> GetByFileNameAsync(string fileName, CancellationToken cancellationToken);
    Task Add(FileImport entity, CancellationToken cancellationToken);
}