using Cads.Cds.Api.Application.Uow;
using Cads.Cds.Api.Infrastructure.Persistence.Contexts;
using Cads.Cds.BuildingBlocks.Testing.Support.Fakes.Transactions;

namespace Cads.Cds.Api.Testing.Support.Fakes.Uow;

public sealed class FakeApiUnitOfWork(ApiWriteDbContext dbContext)
    : FakeManualUnitOfWork<ApiWriteDbContext>(dbContext), IApiUnitOfWork;