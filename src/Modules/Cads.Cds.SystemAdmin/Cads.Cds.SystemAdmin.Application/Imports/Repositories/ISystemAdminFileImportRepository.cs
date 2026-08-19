using Cads.Cds.BuildingBlocks.Application.Imports.Repositories;
using Cads.Cds.ApiSurface.Dtos.Imports;

namespace Cads.Cds.SystemAdmin.Application.Imports.Repositories;

public interface ISystemAdminFileImportRepository : IFileImportRepository 
{
    Task BatchUpdateAsync(string groupKey, long? totalRowsToProcess, long? rowsFound, FileImportStatus? importStatus);
}