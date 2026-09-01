using Cads.Cds.ApiSurface.Dtos.Imports;
using Cads.Cds.BuildingBlocks.Core.Domain.BusinessRules;
using System.Net;

namespace Cads.Cds.BuildingBlocks.Core.Domain.Imports.BusinessRules;

public class MarkCompletedRule(FileImportStatus fileImportStatus) : IBusinessRule
{
    public HttpStatusCode HttpStatusCode => HttpStatusCode.Conflict;

    public bool IsBroken()
    {
        return fileImportStatus is not (
            FileImportStatus.Transferred or
            FileImportStatus.Split);
    }

    public string Message => "Import must be in transferred or split state to complete.";
}