using Cads.Cds.Api.Application.Soap.Messages.Shared;
using CoreWCF;
using System.Xml.Serialization;

namespace Cads.Cds.Api.Application.Soap.Messages.AnimalDetails;

/// <summary>
/// Request model for GetAnimalDetails SOAP operation
/// </summary>
[MessageContract(IsWrapped = false)]
public class GetAnimalDetailsRequest
{
    [MessageBodyMember(Name = "GetAnimalDetailsRequest", Namespace = Namespaces.CattleAnimal, Order = 0)]
    public GetAnimalDetailsRequestBody Body { get; set; } = new();
}


public class GetAnimalDetailsRequestBody
{
    [XmlAttribute("IncludeRecentMovements")]
    public bool IncludeRecentMovements { get; set; }

    [XmlAttribute("IncludeFeedback")]
    public bool IncludeFeedback { get; set; }

    [XmlElement("AnimalsIds", Namespace = Namespaces.CattleAnimal)]
    public AnimalsIds AnimalsIds { get; set; } = new();
}

public class AnimalsIds
{
    [XmlElement("Eartag", Namespace = Namespaces.CattleAnimal)]
    public List<string> Eartag { get; set; } = [];
}