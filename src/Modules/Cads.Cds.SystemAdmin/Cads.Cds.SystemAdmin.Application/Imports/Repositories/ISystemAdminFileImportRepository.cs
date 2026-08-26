using Cads.Cds.ApiSurface.Dtos.Imports;
using Cads.Cds.BuildingBlocks.Application.Imports.Repositories;

namespace Cads.Cds.SystemAdmin.Application.Imports.Repositories;

public interface ISystemAdminFileImportRepository : IFileImportRepository
{
    Task BatchUpdateAsync(string groupKey, long? totalRowsToProcess, long? rowsFound, FileImportStatus? importStatus);
}