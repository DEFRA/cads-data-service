using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtLocation
{
    public string? LocReceivePpafFlag { get; set; }

    public decimal LocId { get; set; }

    public decimal? LocSltId { get; set; }

    public decimal? LocLtyId { get; set; }

    public decimal? LocCtyId { get; set; }

    public string? LocReceiveLabelsFlag { get; set; }

    public DateOnly? LocEffectiveFrom { get; set; }

    public DateOnly? LocEffectiveTo { get; set; }

    public string? LocCessationReason { get; set; }

    public string? LocPremisesType { get; set; }

    public string? LocComments { get; set; }

    public string? LocMapReference { get; set; }

    public string? LocSourceIdentifier { get; set; }

    public string? LocSourceReference { get; set; }

    public string? LocTelNumber { get; set; }

    public string? LocMobileNumber { get; set; }

    public string? LocFaxNumber { get; set; }

    public string? LocEmailAddress { get; set; }

    public string? LocCurrentStatus { get; set; }

    public string? LocCurrentUser { get; set; }

    public DateOnly? LocCurrentModifiedDate { get; set; }

    public decimal? LocCurrentPid { get; set; }

    public string? LocReasonCode { get; set; }

    public decimal? LocVersion { get; set; }

    public decimal FakeData { get; set; }

    public decimal? RowNumber { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? TransId { get; set; }

    public virtual ICollection<CtAddress> CtAddresses { get; set; } = new List<CtAddress>();

    public virtual ICollection<CtAnimalChange> CtAnimalChanges { get; set; } = new List<CtAnimalChange>();

    public virtual ICollection<CtAnimalIdentifier> CtAnimalIdentifiers { get; set; } = new List<CtAnimalIdentifier>();

    public virtual ICollection<CtAnimalRelationship> CtAnimalRelationships { get; set; } = new List<CtAnimalRelationship>();

    public virtual ICollection<CtConditionMarker> CtConditionMarkers { get; set; } = new List<CtConditionMarker>();

    public virtual ICollection<CtEartagStaging> CtEartagStagings { get; set; } = new List<CtEartagStaging>();

    public virtual ICollection<CtEartag> CtEartags { get; set; } = new List<CtEartag>();

    public virtual ICollection<CtIssuedDocument> CtIssuedDocuments { get; set; } = new List<CtIssuedDocument>();

    public virtual ICollection<CtLabelSummary> CtLabelSummaryLasLocIdIdentifyingNavigations { get; set; } = new List<CtLabelSummary>();

    public virtual ICollection<CtLabelSummary> CtLabelSummaryLasLocIdLabelsNavigations { get; set; } = new List<CtLabelSummary>();

    public virtual ICollection<CtLocationIdentifier> CtLocationIdentifiers { get; set; } = new List<CtLocationIdentifier>();

    public virtual ICollection<CtLocationPartyRel> CtLocationPartyRels { get; set; } = new List<CtLocationPartyRel>();

    public virtual ICollection<CtLocationRelationship> CtLocationRelationshipLlrLocIdChildNavigations { get; set; } = new List<CtLocationRelationship>();

    public virtual ICollection<CtLocationRelationship> CtLocationRelationshipLlrLocIdParentNavigations { get; set; } = new List<CtLocationRelationship>();

    public virtual ICollection<CtPpafGrouping> CtPpafGroupingPpgLocIdBirthNavigations { get; set; } = new List<CtPpafGrouping>();

    public virtual ICollection<CtPpafGrouping> CtPpafGroupingPpgLocIdCorresNavigations { get; set; } = new List<CtPpafGrouping>();

    public virtual ICollection<CtRegisteredAnimal> CtRegisteredAnimals { get; set; } = new List<CtRegisteredAnimal>();

    public virtual ICollection<CtRegisteredMovement> CtRegisteredMovements { get; set; } = new List<CtRegisteredMovement>();

    public virtual ICollection<CtSuspConditionMarker> CtSuspConditionMarkers { get; set; } = new List<CtSuspConditionMarker>();

    public virtual ICollection<CtSuspendedAnimal> CtSuspendedAnimalSanLocIdInitialNavigations { get; set; } = new List<CtSuspendedAnimal>();

    public virtual ICollection<CtSuspendedAnimal> CtSuspendedAnimalSanLocIdRequestNavigations { get; set; } = new List<CtSuspendedAnimal>();

    public virtual ICollection<CtValidApplication> CtValidApplications { get; set; } = new List<CtValidApplication>();

    public virtual CtCounty? LocCty { get; set; }

    public virtual CtLocationType? LocLty { get; set; }

    public virtual CtSublocationType? LocSlt { get; set; }
}