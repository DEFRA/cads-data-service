using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtRecdApplicationError1
{
    public long TransId { get; set; }

    public string TransType { get; set; } = null!;

    public decimal RaeId { get; set; }

    public decimal? RaeRapId { get; set; }

    public string? RaeAttributeName { get; set; }

    public string? RaeErrorCode { get; set; }

    public string? RaeCurrentStatus { get; set; }

    public string? RaeCurrentUser { get; set; }

    public DateOnly? RaeCurrentModifiedDate { get; set; }

    public decimal? RaeCurrentPid { get; set; }

    public decimal? RaeVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public long? RaeAudId { get; set; }

    public string? RaeAudType { get; set; }

    public DateTime? RaeAudDatetime { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? CtsFileImportId { get; set; }

    public virtual ICollection<CtRecdApplicationError2> CtRecdApplicationError2s { get; set; } = new List<CtRecdApplicationError2>();

    public virtual CtsFileImport? CtsFileImport { get; set; }
}