using Cads.Cds.BuildingBlocks.Application.Commands;
using MediatR;

namespace Cads.Cds.SystemAdmin.Application.Imports.Commands.ResetFileImport;

public sealed record ResetFileImportCommand(long Id)
    : ISystemAdminCommand<Unit>, ITransactionalCommand, IHasId;