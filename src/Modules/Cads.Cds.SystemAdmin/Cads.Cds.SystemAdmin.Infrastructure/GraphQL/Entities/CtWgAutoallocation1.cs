using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtWgAutoallocation1
{
    public long TransId { get; set; }

    public string TransType { get; set; } = null!;

    public decimal WgaId { get; set; }

    public decimal? WgaRouId { get; set; }

    public decimal? WgaWgpId { get; set; }

    public string? WgaAllocation { get; set; }

    public string? WgaAssignment { get; set; }

    public string? WgaCurrentUser { get; set; }

    public decimal? WgaCurrentPid { get; set; }

    public string? WgaCurrentStatus { get; set; }

    public DateOnly? WgaCurrentModifiedDate { get; set; }

    public decimal? WgaVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public long? WgaAudId { get; set; }

    public string? WgaAudType { get; set; }

    public DateTime? WgaAudDatetime { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? CtsFileImportId { get; set; }

    public virtual ICollection<CtWgAutoallocation2> CtWgAutoallocation2s { get; set; } = new List<CtWgAutoallocation2>();

    public virtual CtsFileImport? CtsFileImport { get; set; }
}
