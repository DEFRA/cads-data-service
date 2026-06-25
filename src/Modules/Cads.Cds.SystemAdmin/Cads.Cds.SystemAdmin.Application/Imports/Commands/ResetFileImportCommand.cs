using Cads.Cds.BuildingBlocks.Application.Commands;
using MediatR;

namespace Cads.Cds.SystemAdmin.Application.Imports.Commands;

public sealed record ResetFileImportCommand(long Id)
    : ICommand<Unit>, ITransactionalCommand, IHasId;