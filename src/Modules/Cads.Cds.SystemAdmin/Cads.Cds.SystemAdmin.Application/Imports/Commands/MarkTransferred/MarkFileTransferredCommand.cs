using Cads.Cds.BuildingBlocks.Application.Commands;
using MediatR;

namespace Cads.Cds.SystemAdmin.Application.Imports.Commands.MarkTransferred;

public sealed record MarkFileTransferredCommand(long Id)
    : ISystemAdminCommand<Unit>, ITransactionalCommand, IHasId;