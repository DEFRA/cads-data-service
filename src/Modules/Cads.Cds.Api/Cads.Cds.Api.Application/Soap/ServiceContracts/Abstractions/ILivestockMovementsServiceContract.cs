using Cads.Cds.Api.Application.Soap.Messages.LivestockMovements;
using Cads.Cds.Api.Application.Soap.Messages.Shared;
using CoreWCF;

namespace Cads.Cds.Api.Application.Soap.ServiceContracts.Abstractions;

/// <summary>
/// Service contract for Livestock movement SOAP operations
/// </summary>
[ServiceContract(Namespace = Namespaces.LivestockMovements)]
public interface ILivestockMovementsServiceContract
{
    [OperationContract(Name = "GetLivestockMovements", Action = "*", ReplyAction = "*")]
    [XmlSerializerFormat]
    GetLivestockMovementsResponse GetLivestockMovements(GetLivestockMovementsRequest request);

}