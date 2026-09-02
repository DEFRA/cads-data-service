using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtAllocRoutine1
{
    public decimal RouId { get; set; }

    public string? RouRoutine { get; set; }

    public string? RouAllocationType { get; set; }

    public string? RouLongDescription { get; set; }

    public string? RouCurrentUser { get; set; }

    public string? RouCurrentStatus { get; set; }

    public DateOnly? RouCurrentModifiedDate { get; set; }

    public decimal? RouCurrentPid { get; set; }

    public decimal? RouVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public long TransId { get; set; }

    public string TransType { get; set; } = null!;

    public long? RouAudId { get; set; }

    public string? RouAudType { get; set; }

    public DateTime? RouAudDatetime { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? CtsFileImportId { get; set; }

    public virtual CtsFileImport? CtsFileImport { get; set; }
}
