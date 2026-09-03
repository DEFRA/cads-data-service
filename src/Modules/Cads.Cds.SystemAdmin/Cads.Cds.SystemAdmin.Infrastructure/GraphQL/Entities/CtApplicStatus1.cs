using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtApplicStatus1
{
    public long TransId { get; set; }

    public string TransType { get; set; } = null!;

    public decimal ApsId { get; set; }

    public decimal? ApsVapId { get; set; }

    public string? ApsUser { get; set; }

    public string? ApsStatus { get; set; }

    public DateOnly? ApsModifiedDate { get; set; }

    public decimal? ApsPid { get; set; }

    public string? ApsIntendedAction { get; set; }

    public decimal? ApsVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public long? ApsAudId { get; set; }

    public string? ApsAudType { get; set; }

    public DateTime? ApsAudDatetime { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? CtsFileImportId { get; set; }

    public virtual ICollection<CtApplicStatus2> CtApplicStatus2s { get; set; } = new List<CtApplicStatus2>();

    public virtual CtsFileImport? CtsFileImport { get; set; }
}