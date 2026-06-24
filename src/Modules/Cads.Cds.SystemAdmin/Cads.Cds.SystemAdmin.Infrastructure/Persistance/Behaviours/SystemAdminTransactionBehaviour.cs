using Cads.Cds.BuildingBlocks.Application.Commands;
using Cads.Cds.BuildingBlocks.Infrastructure.Database.Behaviours;
using Cads.Cds.SystemAdmin.Infrastructure.Persistance.Contexts;

namespace Cads.Cds.SystemAdmin.Infrastructure.Persistance.Behaviours;

public class SystemAdminTransactionBehaviour<TRequest, TResponse>(SystemAdminWriteDbContext dbContext)
    : TransactionBehaviourBase<TRequest, TResponse, SystemAdminWriteDbContext>(dbContext)
    where TRequest : ICommand<TResponse>
{
}