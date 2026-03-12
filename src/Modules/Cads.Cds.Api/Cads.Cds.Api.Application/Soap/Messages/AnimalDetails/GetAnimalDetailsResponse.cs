using System.Xml.Serialization;
using Cads.Cds.Api.Application.Soap.Messages.Shared;
using CoreWCF;

namespace Cads.Cds.Api.Application.Soap.Messages.AnimalDetails;

[XmlRoot(ElementName = "GetAnimalDetailsResponse", Namespace = Namespaces.CattleAnimal)]
[MessageContract(WrapperName = "GetAnimalDetailsResponse", WrapperNamespace = Namespaces.CattleAnimal, IsWrapped = true)]
public class GetAnimalDetailsResponse
{
    [XmlElement("SearchResults", Namespace = Namespaces.CattleAnimal)]
    [MessageBodyMember(Namespace = Namespaces.CattleAnimal, Order = 0)]
    public SearchResults SearchResults { get; set; } = new();
}

public class SearchResults
{
    [XmlElement("EartagResult", Namespace = Namespaces.CattleAnimal)]
    public List<EartagResult> EartagResults { get; set; } = [];
}

public class EartagResult
{
    [XmlElement("DetailsFound", Namespace = Namespaces.CattleAnimal)]
    public DetailsFound DetailsFound { get; set; } = new();

    [XmlElement("Eartag", Namespace = Namespaces.CattleAnimal)]
    public string Eartag { get; set; } = string.Empty;
}

public class DetailsFound
{
    [XmlElement("AnimalRecord", Namespace = Namespaces.CattleAnimal)]
    public AnimalRecord AnimalRecord { get; set; } = new();

    [XmlElement("Movements", Namespace = Namespaces.CattleAnimal)]
    public Movements Movements { get; set; } = new();
}