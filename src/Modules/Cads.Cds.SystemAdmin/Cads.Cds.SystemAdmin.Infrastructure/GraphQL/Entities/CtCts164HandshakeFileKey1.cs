using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtCts164HandshakeFileKey1
{
    public decimal? BjkBatchId { get; set; }

    public decimal? BjkGroupId { get; set; }

    public string? BjkFiletype { get; set; }

    public string? BjkKey { get; set; }

    public DateOnly? BjkModifiedDate { get; set; }

    public long TransId { get; set; }

    public string TransType { get; set; } = null!;

    public long? BjkAudId { get; set; }

    public string? BjkAudType { get; set; }

    public DateTime? BjkAudDatetime { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? CtsFileImportId { get; set; }

    public virtual CtsFileImport? CtsFileImport { get; set; }
}