using Cads.Cds.ApiSurface.Dtos.Imports;
using Cads.Cds.BuildingBlocks.Application.Commands;
using MediatR;

namespace Cads.Cds.SystemAdmin.Application.Imports.Commands.UpdateFileImport;

public sealed record UpdateFileImportCommand(
    long Id,
    long TotalRowsToProcess,
    long RowsFound,
    FileImportStatus ImportStatus)
    : ISystemAdminCommand<Unit>, ITransactionalCommand;