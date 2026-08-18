using Cads.Cds.BuildingBlocks.Application.Commands;
using Cads.Cds.BuildingBlocks.Core.Domain.Imports;

namespace Cads.Cds.SystemAdmin.Application.Imports.Commands.CreateFileImport;

public sealed record CreateFileImportCommand(
    string FileName,
    long TotalRowsToProcess,
    long RowsFound
) : ISystemAdminCommand<FileImport>, ITransactionalCommand;