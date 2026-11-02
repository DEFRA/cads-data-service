using Cads.Cds.BuildingBlocks.Infrastructure.Persistence.Uow;
using Cads.Cds.MiBff.Application.Uow;
using Cads.Cds.MiBff.Infrastructure.Persistence.Contexts;

namespace Cads.Cds.MiBff.Infrastructure.Persistence.Uow;

internal sealed class MiBffUnitOfWork(MiBffWriteDbContext dbContext)
    : ManualUnitOfWork<MiBffWriteDbContext>(dbContext), IMiBffUnitOfWork;