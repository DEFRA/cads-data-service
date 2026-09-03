using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtValidApplication
{
    public decimal VapId { get; set; }

    public string? VapCurrentStatus { get; set; }

    public string? VapCurrentUser { get; set; }

    public DateOnly? VapCurrentModifiedDate { get; set; }

    public decimal? VapCurrentPid { get; set; }

    public string? VapCurrentIntendedAction { get; set; }

    public string? VapApplicationType { get; set; }

    public DateOnly? VapReceiptDate { get; set; }

    public decimal? VapLocIdRequester { get; set; }

    public DateOnly? VapRequesterDate { get; set; }

    public string? VapCountyRequester { get; set; }

    public string? VapSourceType { get; set; }

    public DateOnly? VapTargetDate { get; set; }

    public string? VapSourceReference { get; set; }

    public string? VapCtsIndicator { get; set; }

    public decimal? VapNoOfAnimals { get; set; }

    public decimal? VapNoOfAnimalsNotCanc { get; set; }

    public decimal? VapNumberCalfMovts { get; set; }

    public string? VapInterfaceFileName { get; set; }

    public decimal? VapInterfaceFileTxn { get; set; }

    public decimal? VapWurId { get; set; }

    public decimal? VapVersion { get; set; }

    public string? VapRequesterLocationRepd { get; set; }

    public decimal FakeData { get; set; }

    public decimal? RowNumber { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? TransId { get; set; }

    public virtual ICollection<CtAnimalCorrectSummary> CtAnimalCorrectSummaries { get; set; } = new List<CtAnimalCorrectSummary>();

    public virtual ICollection<CtApplicStatus> CtApplicStatuses { get; set; } = new List<CtApplicStatus>();

    public virtual ICollection<CtRegisteredAnimal> CtRegisteredAnimals { get; set; } = new List<CtRegisteredAnimal>();

    public virtual ICollection<CtSuspendedAnimal> CtSuspendedAnimals { get; set; } = new List<CtSuspendedAnimal>();

    public virtual CtLocation? VapLocIdRequesterNavigation { get; set; }

    public virtual CtWebUser? VapWur { get; set; }
}