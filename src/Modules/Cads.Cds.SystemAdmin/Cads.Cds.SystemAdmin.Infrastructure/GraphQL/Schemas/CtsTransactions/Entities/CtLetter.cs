using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Schemas.CtsTransactions.Entities;

public partial class CtLetter
{
    public long TransId { get; set; }

    public string TransType { get; set; } = null!;

    public decimal LetId { get; set; }

    public string? LetType { get; set; }

    public string? LetDescription { get; set; }

    public decimal? LetWgpId { get; set; }

    public string? LetProgramName { get; set; }

    public decimal? LetWgpIdSent { get; set; }

    public string? LetCurrentUser { get; set; }

    public string? LetCurrentStatus { get; set; }

    public DateOnly? LetCurrentModifiedDate { get; set; }

    public decimal? LetCurrentPid { get; set; }

    public decimal? LetVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public long? LetAudId { get; set; }

    public string? LetAudType { get; set; }

    public DateTime? LetAudDatetime { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? CtsFileImportId { get; set; }
}
