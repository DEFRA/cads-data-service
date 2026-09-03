using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtIssuingAuthority1
{
    public decimal IsaId { get; set; }

    public string? IsaCountryName { get; set; }

    public string? IsaManufacturersName { get; set; }

    public string? IsaType { get; set; }

    public string? IsaCurrentStatus { get; set; }

    public string? IsaCurrentUser { get; set; }

    public DateOnly? IsaCurrentModifiedDate { get; set; }

    public decimal? IsaCurrentPid { get; set; }

    public decimal? IsaVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public long TransId { get; set; }

    public string TransType { get; set; } = null!;

    public long? IsaAudId { get; set; }

    public string? IsaAudType { get; set; }

    public DateTime? IsaAudDatetime { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? CtsFileImportId { get; set; }

    public virtual CtsFileImport? CtsFileImport { get; set; }
}