using Cads.Cds.BuildingBlocks.Testing.Support.Fakes.Transactions;
using Cads.Cds.SystemAdmin.Application.Uow;
using Cads.Cds.SystemAdmin.Infrastructure.Persistance.Contexts;

namespace Cads.Cds.SystemAdmin.Testing.Support.Fakes.Uow;

public sealed class FakeSystemAdminUnitOfWork(SystemAdminWriteDbContext dbContext)
    : FakeManualUnitOfWork<SystemAdminWriteDbContext>(dbContext), ISystemAdminUnitOfWork;