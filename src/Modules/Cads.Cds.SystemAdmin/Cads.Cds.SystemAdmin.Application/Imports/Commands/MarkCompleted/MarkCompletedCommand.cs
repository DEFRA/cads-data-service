using Cads.Cds.BuildingBlocks.Application.Commands;
using MediatR;

namespace Cads.Cds.SystemAdmin.Application.Imports.Commands.MarkCompleted;

public sealed record MarkCompletedCommand(long Id)
    : ISystemAdminCommand<Unit>, ITransactionalCommand, IHasId;