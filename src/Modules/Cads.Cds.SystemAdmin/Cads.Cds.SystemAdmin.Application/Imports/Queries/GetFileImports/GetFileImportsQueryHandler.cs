using Cads.Cds.ApiSurface.Dtos.Imports;
using Cads.Cds.BuildingBlocks.Application.Queries;
using Cads.Cds.SystemAdmin.Application.Imports.Mappings;
using Cads.Cds.SystemAdmin.Application.Imports.Repositories;
using Cads.Cds.SystemAdmin.Application.Imports.Utilities;

namespace Cads.Cds.SystemAdmin.Application.Imports.Queries.GetFileImports;

public sealed class GetFileImportsQueryHandler(     
    ISystemAdminFileImportRepository fileImportRepository)
    : IQueryHandler<GetFileImportsQuery, IEnumerable<FileImportDto>>
{
    public async Task<IEnumerable<FileImportDto>> Handle(GetFileImportsQuery request, CancellationToken cancellationToken)
    {
        var expression = ExpressionBuilder.CreateFilterExpression(request);

        var projected = await fileImportRepository.ProjectAsync(
        q => q.Where(expression)
              .Select(x => x.MapToDto()),
        asNoTracking: true,
        cancellationToken: CancellationToken.None);
        
        return projected;
    }
}