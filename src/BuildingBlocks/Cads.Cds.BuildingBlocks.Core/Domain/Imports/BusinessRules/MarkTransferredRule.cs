using System.Net;
using Cads.Cds.ApiSurface.Dtos.Imports;
using Cads.Cds.BuildingBlocks.Core.Domain.BusinessRules;

namespace Cads.Cds.BuildingBlocks.Core.Domain.Imports.BusinessRules;

public class MarkTransferredRule(FileImportStatus fileImportStatus) : IBusinessRule
{
    public HttpStatusCode HttpStatusCode => HttpStatusCode.Conflict;

    public bool IsBroken()
    {
        return fileImportStatus != FileImportStatus.Pending;
    }

    public string Message => "Transferred can only start from pending.";
}