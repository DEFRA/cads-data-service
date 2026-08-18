using Cads.Cds.ApiSurface.Dtos.Imports;
using Cads.Cds.BuildingBlocks.Core.Domain.BusinessRules;
using System.Net;

namespace Cads.Cds.BuildingBlocks.Core.Domain.Imports.BusinessRules;

public class MarkSplitRule(FileImportStatus fileImportStatus) : IBusinessRule
{
    public HttpStatusCode HttpStatusCode => HttpStatusCode.Conflict;

    public bool IsBroken()
    {
        return fileImportStatus != FileImportStatus.Transferred;
    }

    public string Message => "Split can only start from transferred.";
}