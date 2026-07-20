using Cads.Cds.BuildingBlocks.Application.Commands;
using MediatR;

namespace Cads.Cds.SystemAdmin.Application.Imports.Commands.MarkFailed;

public sealed record MarkFailedCommand(long Id)
    : ISystemAdminCommand<Unit>, ITransactionalCommand, IHasId;