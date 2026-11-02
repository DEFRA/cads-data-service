using Cads.Cds.BuildingBlocks.Testing.Support.Fakes.Transactions;
using Cads.Cds.MiBff.Application.Uow;
using Cads.Cds.MiBff.Infrastructure.Persistence.Contexts;

namespace Cads.Cds.MiBff.Testing.Support.Fakes.Uow;

public class FakeMiBffUnitOfWork(MiBffWriteDbContext dbContext)
    : FakeManualUnitOfWork<MiBffWriteDbContext>(dbContext), IMiBffUnitOfWork;