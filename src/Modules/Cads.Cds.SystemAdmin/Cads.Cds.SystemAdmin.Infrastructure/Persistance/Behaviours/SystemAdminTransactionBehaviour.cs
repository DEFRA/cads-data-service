using Cads.Cds.BuildingBlocks.Infrastructure.Persistence.Behaviours;
using Cads.Cds.SystemAdmin.Application.Imports.Commands;
using Cads.Cds.SystemAdmin.Infrastructure.Persistance.Contexts;
using System.Diagnostics.CodeAnalysis;

namespace Cads.Cds.SystemAdmin.Infrastructure.Persistance.Behaviours;

[ExcludeFromCodeCoverage]
public class SystemAdminTransactionBehaviour<TRequest, TResponse>(SystemAdminWriteDbContext dbContext)
    : TransactionBehaviourBase<TRequest, TResponse, SystemAdminWriteDbContext>(dbContext)
    where TRequest : ISystemAdminCommand<TResponse>
{
}