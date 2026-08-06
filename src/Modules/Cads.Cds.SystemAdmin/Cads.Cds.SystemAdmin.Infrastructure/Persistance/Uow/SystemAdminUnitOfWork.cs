using Cads.Cds.BuildingBlocks.Infrastructure.Persistence.Uow;
using Cads.Cds.SystemAdmin.Application.Uow;
using Cads.Cds.SystemAdmin.Infrastructure.Persistance.Contexts;

namespace Cads.Cds.SystemAdmin.Infrastructure.Persistance.Uow;

internal sealed class SystemAdminUnitOfWork(SystemAdminWriteDbContext dbContext)
    : ManualUnitOfWork<SystemAdminWriteDbContext>(dbContext), ISystemAdminUnitOfWork;