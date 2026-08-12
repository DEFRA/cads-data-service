using System.Net;
using Cads.Cds.ApiSurface.Dtos.Imports;
using Cads.Cds.BuildingBlocks.Core.Domain.BusinessRules;

namespace Cads.Cds.BuildingBlocks.Core.Domain.Imports.BusinessRules;

public class MarkFailedRule(FileImportStatus fileImportStatus): IBusinessRule
{
    public HttpStatusCode HttpStatusCode => HttpStatusCode.Conflict;

    public bool IsBroken()
    {
        return fileImportStatus == FileImportStatus.Completed;
    }

    public string Message => "Import must be in pending, transferred, or split state to be marked as failed.";
}