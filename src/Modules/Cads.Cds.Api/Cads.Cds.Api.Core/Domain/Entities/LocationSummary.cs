namespace Cads.Cds.Api.Core.Domain.Entities;

public class LocationSummary
{
    public string? LidIdentifier { get; set; }
    public string? LidFullIdentifier { get; set; }
    public string? LidSubIdentifier { get; set; }
    public DateOnly? LidEffectiveFromDate { get; set; }
    public DateOnly? LidEffectiveToDate { get; set; }
    public DateOnly? LidCurrentModifiedDate { get; set; }

    public string? LtyShortDescription { get; set; }
    public string? LtyLongDescription { get; set; }

    public string? LocMapReference { get; set; }
    public DateOnly? LocEffectiveFrom { get; set; }
    public DateOnly? LocEffectiveTo { get; set; }
    public string? LocCessationReason { get; set; }
    public string? LocComments { get; set; }
    public string? LocSourceIdentifier { get; set; }
    public string? LocTelNumber { get; set; }
    public string? LocMobileNumber { get; set; }
    public string? LocFaxNumber { get; set; }
    public string? LocEmailAddress { get; set; }
    public DateOnly? LocCurrentModifiedDate { get; set; }
    public string? LocReasonCode { get; set; }
    public decimal? LocVersion { get; set; }

    public string? CtyName { get; set; }
    public string? CtyVetAreaDesc { get; set; }
    public string? CtyPassportAreaDesc { get; set; }
    public string? CtyAdminOffice { get; set; }
    public string? CtyBcmsTeam { get; set; }
    public string? CtyInspectionArea { get; set; }
    public string? CtyDataMgtAreaDesc { get; set; }
    public string? CtyCurrentStatus { get; set; }

    public string? LifDescription { get; set; }
    public DateTime? ImportedDate { get; set; }
}