using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtAnimalStatus
{
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

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? TransId { get; set; }

    public virtual CtRegisteredAnimal? AstRan { get; set; }
}
