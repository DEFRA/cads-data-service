using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtAnimalStatus1
{
    public long TransId { get; set; }

    public string TransType { get; set; } = null!;

    public decimal AstId { get; set; }

    public decimal? AstRanId { get; set; }

    public string? AstStatus { get; set; }

    public string? AstUser { get; set; }

    public DateOnly? AstModifiedDate { get; set; }

    public decimal? AstPid { get; set; }

    public string? AstIntendedAction { get; set; }

    public DateOnly? AstChangeReceivedDate { get; set; }

    public decimal? AstTracedMoves { get; set; }

    public decimal? AstAddMoves { get; set; }

    public decimal? AstVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public long? AstAudId { get; set; }

    public string? AstAudType { get; set; }

    public DateTime? AstAudDatetime { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? CtsFileImportId { get; set; }

    public virtual ICollection<CtAnimalStatus2> CtAnimalStatus2s { get; set; } = new List<CtAnimalStatus2>();

    public virtual CtsFileImport? CtsFileImport { get; set; }
}