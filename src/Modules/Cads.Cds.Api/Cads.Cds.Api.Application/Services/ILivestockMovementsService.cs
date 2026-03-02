using Cads.Cds.Api.Application.Soap;
using CoreWCF;

namespace Cads.Cds.Api.Application.Services;

/// <summary>
/// Service contract for Livestock Movements SOAP operations
/// </summary>
[ServiceContract(Namespace = "http://services.defra.gov.uk/ahw/livestockmovements")]
public interface ILivestockMovementsService
{
    [OperationContract(Action = "*", ReplyAction = "*")]
    Task<GetAnimalCohortResponse> GetAnimalCohortRequest(GetAnimalCohortRequest request);
}
