using Cads.Cds.ApiSurface.Dtos.Imports;
using Cads.Cds.BuildingBlocks.Application.Queries;
using Cads.Cds.BuildingBlocks.Core.Extensions;
using Cads.Cds.SystemAdmin.Application.Imports.Repositories;
using Cads.Cds.SystemAdmin.Application.Imports.Mappings;

namespace Cads.Cds.SystemAdmin.Application.Imports.Queries.GetFileImportByFileName;

public sealed class GetFileImportByFileNameQueryHandler(
    ISystemAdminFileImportRepository fileImportRepository)
    : IQueryHandler<GetFileImportByFileNameQuery, FileImportDto?>
{
    public async Task<FileImportDto?> Handle(GetFileImportByFileNameQuery query, CancellationToken cancellationToken)
    {
        var fileName = query.FileName.NormalizeToUpper()!;
        var fileImport = await fileImportRepository.GetByFileNameAsync(fileName, cancellationToken);

        return fileImport?.MapToDto();
    }
}