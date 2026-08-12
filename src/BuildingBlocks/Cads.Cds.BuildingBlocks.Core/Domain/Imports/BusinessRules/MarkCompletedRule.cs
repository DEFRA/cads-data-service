using System.Net;
using Cads.Cds.ApiSurface.Dtos.Imports;
using Cads.Cds.BuildingBlocks.Core.Domain.BusinessRules;

namespace Cads.Cds.BuildingBlocks.Core.Domain.Imports.BusinessRules;

public class MarkCompletedRule(FileImportStatus fileImportStatus) : IBusinessRule
{
    public HttpStatusCode HttpStatusCode => HttpStatusCode.Conflict;

    public bool IsBroken()
    {
        return fileImportStatus != FileImportStatus.Split;
    }

    public string Message => "Import must be in split state to complete.";
}