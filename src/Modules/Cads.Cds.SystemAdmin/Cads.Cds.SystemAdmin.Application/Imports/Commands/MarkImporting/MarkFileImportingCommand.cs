using Cads.Cds.BuildingBlocks.Application.Commands;
using MediatR;

namespace Cads.Cds.SystemAdmin.Application.Imports.Commands.MarkImporting;

public sealed record MarkFileImportingCommand(long Id)
    : ICommand<Unit>, ITransactionalCommand, IHasId;