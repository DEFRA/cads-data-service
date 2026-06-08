using System.Text.Json.Serialization;

namespace Cads.Cds.Api.Core.DTOs;

public class LocationDto
{
    [JsonPropertyName("lidIdentifier")]
    public string? LidIdentifier { get; set; }

    [JsonPropertyName("lidFullIdentifier")]
    public string? LidFullIdentifier { get; set; }

    [JsonPropertyName("lidSubIdentifier")]
    public string? LidSubIdentifier { get; set; }

    [JsonPropertyName("lidEffectiveFromDate")]
    public DateOnly? LidEffectiveFromDate { get; set; }

    [JsonPropertyName("lidEffectiveToDate")]
    public DateOnly? LidEffectiveToDate { get; set; }

    [JsonPropertyName("lidCurrentModifiedDate")]
    public DateOnly? LidCurrentModifiedDate { get; set; }

    [JsonPropertyName("ltyShortDescription")]
    public string? LtyShortDescription { get; set; }

    [JsonPropertyName("ltyLongDescription")]
    public string? LtyLongDescription { get; set; }

    [JsonPropertyName("locMapReference")]
    public string? LocMapReference { get; set; }

    [JsonPropertyName("locEffectiveFrom")]
    public DateOnly? LocEffectiveFrom { get; set; }

    [JsonPropertyName("locEffectiveTo")]
    public DateOnly? LocEffectiveTo { get; set; }

    [JsonPropertyName("locCessationReason")]
    public string? LocCessationReason { get; set; }

    [JsonPropertyName("locComments")]
    public string? LocComments { get; set; }

    [JsonPropertyName("locSourceIdentifier")]
    public string? LocSourceIdentifier { get; set; }

    [JsonPropertyName("locTelNumber")]
    public string? LocTelNumber { get; set; }

    [JsonPropertyName("locMobileNumber")]
    public string? LocMobileNumber { get; set; }

    [JsonPropertyName("locFaxNumber")]
    public string? LocFaxNumber { get; set; }

    [JsonPropertyName("locEmailAddress")]
    public string? LocEmailAddress { get; set; }

    [JsonPropertyName("locCurrentModifiedDate")]
    public DateOnly? LocCurrentModifiedDate { get; set; }

    [JsonPropertyName("locReasonCode")]
    public string? LocReasonCode { get; set; }

    [JsonPropertyName("locVersion")]
    public decimal? LocVersion { get; set; }

    [JsonPropertyName("ctyName")]
    public string? CtyName { get; set; }

    [JsonPropertyName("ctyVetAreaDesc")]
    public string? CtyVetAreaDesc { get; set; }

    [JsonPropertyName("ctyPassportAreaDesc")]
    public string? CtyPassportAreaDesc { get; set; }

    [JsonPropertyName("ctyAdminOffice")]
    public string? CtyAdminOffice { get; set; }

    [JsonPropertyName("ctyBcmsTeam")]
    public string? CtyBcmsTeam { get; set; }

    [JsonPropertyName("ctyInspectionArea")]
    public string? CtyInspectionArea { get; set; }

    [JsonPropertyName("ctyDataMgtAreaDesc")]
    public string? CtyDataMgtAreaDesc { get; set; }

    [JsonPropertyName("ctyCurrentStatus")]
    public string? CtyCurrentStatus { get; set; }

    [JsonPropertyName("lifDescription")]
    public string? LifDescription { get; set; }

    [JsonPropertyName("importedDate")]
    public DateTime? ImportedDate { get; set; }
}