using Cads.Cds.Api.Application.Soap.Messages.Shared;
using CoreWCF;
using System.Xml.Serialization;

namespace Cads.Cds.Api.Application.Soap.Messages.AnimalPassportAndDetails;

[XmlRoot(ElementName = "GetAnimalPassportAndDetailsResponse", Namespace = Namespaces.AnimalPassport)]
[MessageContract(WrapperName = "GetAnimalPassportAndDetailsResponse", WrapperNamespace = Namespaces.AnimalPassport, IsWrapped = true)]
public class GetAnimalPassportAndDetailsResponse
{
    [XmlElement("SearchResults", Namespace = Namespaces.AnimalPassport)]
    [MessageBodyMember(Namespace = Namespaces.AnimalPassport, Order = 0)]
    public SearchResults? SearchResults { get; set; }
}

public class SearchResults
{
    [XmlElement("EartagResult", Namespace = Namespaces.AnimalPassport)]
    public List<EartagResult> EartagResults { get; set; } = [];
}

public class EartagResult
{
    [XmlElement("DetailsFound", Namespace = Namespaces.AnimalPassport)]
    public DetailsFound? DetailsFound { get; set; }

    [XmlElement("Eartag", Namespace = Namespaces.CattleAnimal)]
    public string Eartag { get; set; } = string.Empty;
}

public class DetailsFound
{
    [XmlElement("AnimalRecord", Namespace = Namespaces.AnimalPassport)]
    public AnimalPassportRecord? AnimalRecord { get; set; }

    [XmlElement("AnimalPassport", Namespace = Namespaces.AnimalPassport)]
    public AnimalPassport? AnimalPassport { get; set; }

    [XmlElement("Movements", Namespace = Namespaces.AnimalPassport)]
    public Movements? Movements { get; set; }
}

public class AnimalPassport
{
    [XmlElement("BirthDam", Namespace = Namespaces.AnimalPassportTypes)]
    public string BirthDam { get; set; } = string.Empty;

    [XmlElement("PassportStatusCode", Namespace = Namespaces.AnimalPassportTypes)]
    public int PassportStatusCode { get; set; }

    [XmlElement("DateOfRegistration", Namespace = Namespaces.AnimalPassportTypes)]
    public string DateOfRegistration { get; set; } = string.Empty;

    [XmlElement("PassportVersion", Namespace = Namespaces.AnimalPassportTypes)]
    public int PassportVersion { get; set; }

    [XmlElement("RefusedPassport", Namespace = Namespaces.AnimalPassportTypes)]
    public bool RefusedPassport { get; set; }

    [XmlElement("DamIsFemale", Namespace = Namespaces.AnimalPassportTypes)]
    public bool DamIsFemale { get; set; }

    [XmlElement("DamAliveAtBirth", Namespace = Namespaces.AnimalPassportTypes)]
    public bool DamAliveAtBirth { get; set; }

    [XmlElement("DamMinimumAgeAtBirth", Namespace = Namespaces.AnimalPassportTypes)]
    public bool DamMinimumAgeAtBirth { get; set; }

    [XmlElement("DamOnLocationAtBirth", Namespace = Namespaces.AnimalPassportTypes)]
    public bool DamOnLocationAtBirth { get; set; }
}