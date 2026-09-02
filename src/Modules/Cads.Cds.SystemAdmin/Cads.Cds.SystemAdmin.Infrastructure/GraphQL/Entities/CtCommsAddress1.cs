using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtCommsAddress1
{
    public long TransId { get; set; }

    public string TransType { get; set; } = null!;

    public decimal CoaId { get; set; }

    public string? CoaCurrentStatus { get; set; }

    public string? CoaCurrentUser { get; set; }

    public DateOnly? CoaCurrentModifiedDate { get; set; }

    public decimal? CoaPid { get; set; }

    public string? CoaEmailAddress { get; set; }

    public char? CoaAttachment { get; set; }

    public decimal? RowNumber { get; set; }

    public long? CoaAudId { get; set; }

    public string? CoaAudType { get; set; }

    public DateTime? CoaAudDatetime { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? CtsFileImportId { get; set; }

    public virtual ICollection<CtCommsAddress2> CtCommsAddress2s { get; set; } = new List<CtCommsAddress2>();

    public virtual CtsFileImport? CtsFileImport { get; set; }
}
