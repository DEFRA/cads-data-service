using Cads.Cds.ApiSurface.Dtos.Imports;
using Cads.Cds.BuildingBlocks.Infrastructure.Imports.Repositories;
using Cads.Cds.SystemAdmin.Application.Imports.Repositories;
using Cads.Cds.SystemAdmin.Infrastructure.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Cads.Cds.SystemAdmin.Infrastructure.Persistance.Repositories;

public class SystemAdminFileImportRepository(
    SystemAdminReadDbContext readDbContext,
    SystemAdminWriteDbContext writeDbContext)
    : FileImportRepository<SystemAdminReadDbContext, SystemAdminWriteDbContext>(readDbContext, writeDbContext), ISystemAdminFileImportRepository
{
    public async Task BatchUpdateAsync(string groupKey, long? totalRowsToProcess, long? rowsFound, FileImportStatus? importStatus)
    {
        var items = Set();

        await items
            .Where(i => i.GroupKey == groupKey)
            .ExecuteUpdateAsync(setters => setters
                .SetProperty(l => l.TotalRowsToProcess,
                    r => totalRowsToProcess ?? r.TotalRowsToProcess)
                .SetProperty(l => l.RowsFound,
                    r => rowsFound != null ? rowsFound.Value : r.RowsFound)
                .SetProperty(l => l.ImportStatus,
                    r => importStatus != null ? importStatus.Value : r.ImportStatus)
            );
    }
}