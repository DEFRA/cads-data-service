using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtWgUserAssignment1
{
    public long TransId { get; set; }

    public string TransType { get; set; } = null!;

    public decimal WuaId { get; set; }

    public decimal? WuaCusId { get; set; }

    public decimal? WuaWgpId { get; set; }

    public string? WuaWgContactInd { get; set; }

    public string? WuaFavouredWgInd { get; set; }

    public string? WuaCurrentUser { get; set; }

    public string? WuaCurrentStatus { get; set; }

    public DateOnly? WuaCurrentModifiedDate { get; set; }

    public decimal? WuaCurrentPid { get; set; }

    public decimal? WuaVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public long? WuaAudId { get; set; }

    public string? WuaAudType { get; set; }

    public DateTime? WuaAudDatetime { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? CtsFileImportId { get; set; }

    public virtual ICollection<CtWgUserAssignment2> CtWgUserAssignment2s { get; set; } = new List<CtWgUserAssignment2>();

    public virtual CtsFileImport? CtsFileImport { get; set; }
}