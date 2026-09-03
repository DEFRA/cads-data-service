using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtRegisteredAnimal
{
    public decimal RanId { get; set; }

    public string? RanCurrentUser { get; set; }

    public string? RanCurrentStatus { get; set; }

    public DateOnly? RanCurrentModifiedDate { get; set; }

    public decimal? RanCurrentPid { get; set; }

    public string? RanCurrentIntendedAction { get; set; }

    public DateOnly? RanCurrentChangeRcvdDate { get; set; }

    public decimal? RanCurrentTracedMoves { get; set; }

    public decimal? RanCurrentAddMoves { get; set; }

    public string? RanCtsIndicator { get; set; }

    public string? RanPassportOrLicence { get; set; }

    public string? RanSex { get; set; }

    public DateOnly? RanBirthDate { get; set; }

    public decimal? RanApplicLine { get; set; }

    public decimal? RanBrdId { get; set; }

    public decimal? RanLocIdPassport { get; set; }

    public decimal? RanVapId { get; set; }

    public decimal? RanMovIdRegistration { get; set; }

    public string? RanPassportModFlag { get; set; }

    public string? RanPassportVersionNumber { get; set; }

    public decimal? RanVersion { get; set; }

    public decimal? RanMovIdDeath { get; set; }

    public decimal? RanCryIdChrOrigin { get; set; }

    public string? RanPassportLocationRepd { get; set; }

    public decimal FakeData { get; set; }

    public decimal? RowNumber { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? TransId { get; set; }

    public virtual ICollection<CtAnimalChange> CtAnimalChanges { get; set; } = new List<CtAnimalChange>();

    public virtual ICollection<CtAnimalClaim> CtAnimalClaims { get; set; } = new List<CtAnimalClaim>();

    public virtual ICollection<CtAnimalCorrectSummary> CtAnimalCorrectSummaries { get; set; } = new List<CtAnimalCorrectSummary>();

    public virtual ICollection<CtAnimalIdentifier> CtAnimalIdentifiers { get; set; } = new List<CtAnimalIdentifier>();

    public virtual ICollection<CtAnimalRelationship> CtAnimalRelationshipAarRanIdChildNavigations { get; set; } = new List<CtAnimalRelationship>();

    public virtual ICollection<CtAnimalRelationship> CtAnimalRelationshipAarRanIdParentNavigations { get; set; } = new List<CtAnimalRelationship>();

    public virtual ICollection<CtAnimalStatus> CtAnimalStatuses { get; set; } = new List<CtAnimalStatus>();

    public virtual ICollection<CtConditionMarker> CtConditionMarkers { get; set; } = new List<CtConditionMarker>();

    public virtual ICollection<CtIssuedDocument> CtIssuedDocuments { get; set; } = new List<CtIssuedDocument>();

    public virtual ICollection<CtMgtControlError> CtMgtControlErrors { get; set; } = new List<CtMgtControlError>();

    public virtual ICollection<CtReceivedApplication> CtReceivedApplications { get; set; } = new List<CtReceivedApplication>();

    public virtual ICollection<CtRegisteredMovement> CtRegisteredMovements { get; set; } = new List<CtRegisteredMovement>();

    public virtual ICollection<CtSuspConditionMarker> CtSuspConditionMarkers { get; set; } = new List<CtSuspConditionMarker>();

    public virtual ICollection<CtSuspendedAnimal> CtSuspendedAnimals { get; set; } = new List<CtSuspendedAnimal>();

    public virtual CtBreed? RanBrd { get; set; }

    public virtual CtCountry? RanCryIdChrOriginNavigation { get; set; }

    public virtual CtLocation? RanLocIdPassportNavigation { get; set; }

    public virtual CtValidApplication? RanVap { get; set; }
}