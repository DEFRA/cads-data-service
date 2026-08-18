using Cads.Cds.BuildingBlocks.Infrastructure.Persistence.Uow;
using Cads.Cds.Ingester.Application.Uow;
using Cads.Cds.Ingester.Infrastructure.Persistence.Contexts;

namespace Cads.Cds.Ingester.Infrastructure.Persistence.Uow;

internal sealed class IngesterUnitOfWork(IngesterWriteDbContext dbContext)
    : ManualUnitOfWork<IngesterWriteDbContext>(dbContext), IIngesterUnitOfWork;