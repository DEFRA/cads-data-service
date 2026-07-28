using Cads.Cds.BuildingBlocks.Application.Commands;
using MediatR;

namespace Cads.Cds.SystemAdmin.Application.Imports.Commands.MarkFailed;

public sealed record MarkFailedCommand(long Id, string Reason)
    : ISystemAdminCommand<Unit>, ITransactionalCommand, IHasId;