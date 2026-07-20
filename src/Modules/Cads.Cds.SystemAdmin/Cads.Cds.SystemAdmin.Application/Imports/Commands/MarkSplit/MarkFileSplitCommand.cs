using Cads.Cds.BuildingBlocks.Application.Commands;
using MediatR;

namespace Cads.Cds.SystemAdmin.Application.Imports.Commands.MarkSplit;

public sealed record MarkFileSplitCommand(long Id)
    : ISystemAdminCommand<Unit>, ITransactionalCommand, IHasId;