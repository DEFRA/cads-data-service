using System.ServiceModel;
using CoreWCF;

namespace Cads.Cds.Api.Application.Soap;

/// <summary>
/// Response model for GetAnimalCohort SOAP operation
/// </summary>
[MessageContract(WrapperName = "GetAnimalCohortResponse", WrapperNamespace = "http://services.defra.gov.uk/ahw/livestockmovements", IsWrapped = true)]
public class GetAnimalCohortResponse
{
    [MessageBodyMember(Namespace = "")]
    public string CohortAnimals { get; set; } = string.Empty;

    [MessageBodyMember(Namespace = "")]
    public TraceIdentifier? TraceIdentifier { get; set; }
}
