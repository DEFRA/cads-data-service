using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtLocationRelationship1
{
    public long TransId { get; set; }

    public string TransType { get; set; } = null!;

    public decimal LlrId { get; set; }

    public decimal? LlrLocIdParent { get; set; }

    public decimal? LlrLocIdChild { get; set; }

    public DateOnly? LlrEffectiveFromDate { get; set; }

    public string? LlrCessationReason { get; set; }

    public string? LlrComments { get; set; }

    public decimal? LlrLrtId { get; set; }

    public DateOnly? LlrEffectiveToDate { get; set; }

    public string? LlrCurrentStatus { get; set; }

    public DateOnly? LlrCurrentModifiedDate { get; set; }

    public string? LlrCurrentUser { get; set; }

    public decimal? LlrCurrentPid { get; set; }

    public decimal? LlrVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public long? LlrAudId { get; set; }

    public string? LlrAudType { get; set; }

    public DateTime? LlrAudDatetime { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? CtsFileImportId { get; set; }

    public virtual ICollection<CtLocationRelationship2> CtLocationRelationship2s { get; set; } = new List<CtLocationRelationship2>();

    public virtual CtsFileImport? CtsFileImport { get; set; }
}
