using Cads.Cds.BuildingBlocks.Core.Domain.Imports;

namespace Cads.Cds.SystemAdmin.Application.Imports.Repositories;

public interface IFileImportRepository
{
    Task<FileImport?> GetById(long id, CancellationToken cancellationToken);
    Task<FileImport?> GetByFileName(string fileName, CancellationToken cancellationToken);
    Task Add(FileImport entity, CancellationToken cancellationToken);
}