using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtApplicationLateDay1
{
    public long TransId { get; set; }

    public string TransType { get; set; } = null!;

    public decimal AldId { get; set; }

    public decimal? AldValidDays { get; set; }

    public DateOnly? AldEffectiveFromDate { get; set; }

    public string? AldApplicationType { get; set; }

    public decimal? AldAdditionalDaysLate { get; set; }

    public string? AldCurrentUser { get; set; }

    public string? AldCurrentStatus { get; set; }

    public decimal? AldCurrentPid { get; set; }

    public DateOnly? AldCurrentModifiedDate { get; set; }

    public decimal? AldVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public long? AldAudId { get; set; }

    public string? AldAudType { get; set; }

    public DateTime? AldAudDatetime { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? CtsFileImportId { get; set; }

    public virtual ICollection<CtApplicationLateDay2> CtApplicationLateDay2s { get; set; } = new List<CtApplicationLateDay2>();

    public virtual CtsFileImport? CtsFileImport { get; set; }
}
