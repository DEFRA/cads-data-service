using Cads.Cds.ApiSurface.Dtos.Imports;
using MediatR;

namespace Cads.Cds.SystemAdmin.Application.Imports.Commands.BatchUpdateFileImport;

public sealed record BatchUpdateFileImportCommand(
    string GroupKey,
    long? TotalRowsToProcess,
    long? RowsFound,
    FileImportStatus? ImportStatus)
    : ISystemAdminCommand<Unit>;