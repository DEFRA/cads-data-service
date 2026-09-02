using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtSuspAnimalError1
{
    public long TransId { get; set; }

    public string TransType { get; set; } = null!;

    public decimal SaeId { get; set; }

    public decimal? SaeSanId { get; set; }

    public string? SaeErrorCode { get; set; }

    public string? SaeAttributeName { get; set; }

    public DateOnly? SaeCurrentModifiedDate { get; set; }

    public string? SaeCurrentUser { get; set; }

    public string? SaeCurrentStatus { get; set; }

    public decimal? SaeCurrentPid { get; set; }

    public decimal? SaeVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public long? SaeAudId { get; set; }

    public string? SaeAudType { get; set; }

    public DateTime? SaeAudDatetime { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? CtsFileImportId { get; set; }

    public virtual ICollection<CtSuspAnimalError2> CtSuspAnimalError2s { get; set; } = new List<CtSuspAnimalError2>();

    public virtual CtsFileImport? CtsFileImport { get; set; }
}
