using Cads.Cds.BuildingBlocks.Infrastructure.Imports.Repositories;
using Cads.Cds.SystemAdmin.Application.Imports.Repositories;
using Cads.Cds.SystemAdmin.Infrastructure.Persistance.Contexts;

namespace Cads.Cds.SystemAdmin.Infrastructure.Persistance.Repositories;

public class SystemAdminFileImportRepository(
    SystemAdminReadDbContext readDbContext,
    SystemAdminWriteDbContext writeDbContext)
    : FileImportRepository<SystemAdminReadDbContext, SystemAdminWriteDbContext>(readDbContext, writeDbContext), ISystemAdminFileImportRepository
{
}