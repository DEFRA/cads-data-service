using Cads.Cds.BuildingBlocks.Testing.Support.Fakes.Transactions;
using Cads.Cds.Ingester.Application.Uow;
using Cads.Cds.Ingester.Infrastructure.Persistence.Contexts;

namespace Cads.Cds.Ingester.Testing.Support.Fakes.Uow;

public sealed class FakeIngesterUnitOfWork(IngesterWriteDbContext dbContext)
    : FakeManualUnitOfWork<IngesterWriteDbContext>(dbContext), IIngesterUnitOfWork;