using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtAnimalIdentifier1
{
    public long TransId { get; set; }

    public string TransType { get; set; } = null!;

    public decimal AidId { get; set; }

    public string? AidIdentifier { get; set; }

    public string? AidIdentifierType { get; set; }

    public DateOnly? AidEffectiveFromDate { get; set; }

    public DateOnly? AidEffectiveToDate { get; set; }

    public decimal? AidLocIdAssigned { get; set; }

    public string? AidCurrentFlag { get; set; }

    public decimal? AidRanId { get; set; }

    public decimal? AidEtgId { get; set; }

    public decimal? AidEidId { get; set; }

    public string? AidCurrentUser { get; set; }

    public string? AidCurrentStatus { get; set; }

    public DateOnly? AidCurrentModifiedDate { get; set; }

    public decimal? AidCurrentPid { get; set; }

    public decimal? AidAidIdOriginal { get; set; }

    public decimal? AidAidIdPrevious { get; set; }

    public decimal? AidVersion { get; set; }

    public string? AidAssignedLocationRepd { get; set; }

    public decimal? RowNumber { get; set; }

    public long? AidAudId { get; set; }

    public string? AidAudType { get; set; }

    public DateTime? AidAudDatetime { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? CtsFileImportId { get; set; }

    public virtual ICollection<CtAnimalIdentifier2> CtAnimalIdentifier2s { get; set; } = new List<CtAnimalIdentifier2>();

    public virtual CtsFileImport? CtsFileImport { get; set; }
}