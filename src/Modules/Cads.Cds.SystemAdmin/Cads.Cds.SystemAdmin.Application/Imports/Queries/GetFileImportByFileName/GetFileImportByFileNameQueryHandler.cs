using Cads.Cds.ApiSurface.Dtos.Imports;
using Cads.Cds.BuildingBlocks.Application.Queries;
using Cads.Cds.SystemAdmin.Application.Imports.Mappings;
using Cads.Cds.SystemAdmin.Application.Imports.Repositories;

namespace Cads.Cds.SystemAdmin.Application.Imports.Queries.GetFileImportByFileName;

public sealed class GetFileImportByFileNameQueryHandler(
    ISystemAdminFileImportRepository fileImportRepository)
    : IQueryHandler<GetFileImportByFileNameQuery, FileImportDto?>
{
    public async Task<FileImportDto?> Handle(GetFileImportByFileNameQuery query, CancellationToken cancellationToken)
    {
        var fileImport = await fileImportRepository.GetByFileNameAsync(query.FileName, cancellationToken);

        return fileImport?.MapToDto();
    }
}