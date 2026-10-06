using Cads.Cds.ApiSurface.Dtos.Imports;
using Cads.Cds.BuildingBlocks.Application.Queries;
using Cads.Cds.SystemAdmin.Application.Imports.Mappings;
using Cads.Cds.SystemAdmin.Application.Imports.Repositories;

namespace Cads.Cds.SystemAdmin.Application.Imports.Queries.GetFileImportById;

public sealed class GetFileImportByIdQueryHandler(
    ISystemAdminFileImportRepository fileImportRepository)
    : IQueryHandler<GetFileImportByIdQuery, IEnumerable<FileImportDto>>
{
    public async Task<IEnumerable<FileImportDto>> Handle(GetFileImportByIdQuery request, CancellationToken cancellationToken)
    {
        if (!request.IncludeSiblings)
        {
            return await fileImportRepository.ProjectAsync(
                q => q.Where(x => x.Id == request.Id)
                      .Select(x => x.MapToDto()),
                cancellationToken: cancellationToken);
        }

        // Single query: WHERE GroupKey = (SELECT GroupKey FROM ... WHERE Id = @id)
        return await fileImportRepository.ProjectAsync(
            q => q.Where(x => x.GroupKey == q.Where(y => y.Id == request.Id)
                                             .Select(y => y.GroupKey)
                                             .FirstOrDefault())
                  .Select(x => x.MapToDto()),
            cancellationToken: cancellationToken);
    }
}