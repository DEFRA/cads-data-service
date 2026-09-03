using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtWgSuperAssignment1
{
    public long TransId { get; set; }

    public string TransType { get; set; } = null!;

    public decimal WsaId { get; set; }

    public decimal? WsaWgpIdCurrent { get; set; }

    public decimal? WsaWgpIdAssigned { get; set; }

    public decimal? WsaRouId { get; set; }

    public string? WsaCurrentUser { get; set; }

    public string? WsaCurrentStatus { get; set; }

    public DateOnly? WsaCurrentModifiedDate { get; set; }

    public decimal? WsaCurrentPid { get; set; }

    public decimal? WsaVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public long? WsaAudId { get; set; }

    public string? WsaAudType { get; set; }

    public DateTime? WsaAudDatetime { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? CtsFileImportId { get; set; }

    public virtual ICollection<CtWgSuperAssignment2> CtWgSuperAssignment2s { get; set; } = new List<CtWgSuperAssignment2>();

    public virtual CtsFileImport? CtsFileImport { get; set; }
}