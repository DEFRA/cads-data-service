using Cads.Cds.BuildingBlocks.Application.Commands;
using MediatR;

namespace Cads.Cds.SystemAdmin.Application.Imports.Commands;

public sealed record MarkFileImportFailedCommand(long Id)
    : ICommand<Unit>, ITransactionalCommand, IHasId;