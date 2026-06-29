using Cads.Cds.BuildingBlocks.Application.Commands;
using MediatR;

namespace Cads.Cds.SystemAdmin.Application.Imports.Commands.MarkFileImportFailed;

public sealed record MarkFileImportFailedCommand(long Id)
    : ISystemAdminCommand<Unit>, ITransactionalCommand, IHasId;