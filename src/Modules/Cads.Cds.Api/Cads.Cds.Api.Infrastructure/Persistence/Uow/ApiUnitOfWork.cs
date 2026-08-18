using Cads.Cds.Api.Application.Uow;
using Cads.Cds.Api.Infrastructure.Persistence.Contexts;
using Cads.Cds.BuildingBlocks.Infrastructure.Persistence.Uow;

namespace Cads.Cds.Api.Infrastructure.Persistence.Uow;

internal sealed class ApiUnitOfWork(ApiWriteDbContext dbContext)
    : ManualUnitOfWork<ApiWriteDbContext>(dbContext), IApiUnitOfWork;