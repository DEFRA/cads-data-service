using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtAnimalClaim1
{
    public long TransId { get; set; }

    public string TransType { get; set; } = null!;

    public decimal AncId { get; set; }

    public decimal? AncRanId { get; set; }

    public decimal? AncClaimSequence { get; set; }

    public DateOnly? AncCurrentModifiedDate { get; set; }

    public decimal? AncCurrentPid { get; set; }

    public string? AncCurrentUser { get; set; }

    public decimal? AncClsId { get; set; }

    public decimal? AncCltId { get; set; }

    public string? AncClaimReference { get; set; }

    public DateOnly? AncRetentionStartDate { get; set; }

    public DateOnly? AncRetentionEndDate { get; set; }

    public string? AncOffice { get; set; }

    public decimal? AncSchemeYear { get; set; }

    public DateOnly? AncSchemeModifiedDatetime { get; set; }

    public decimal? AncVersion { get; set; }

    public string? AncCurrentStatus { get; set; }

    public decimal? RowNumber { get; set; }

    public long? AncAudId { get; set; }

    public string? AncAudType { get; set; }

    public DateTime? AncAudDatetime { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? CtsFileImportId { get; set; }

    public virtual ICollection<CtAnimalClaim2> CtAnimalClaim2s { get; set; } = new List<CtAnimalClaim2>();

    public virtual CtsFileImport? CtsFileImport { get; set; }
}