using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Schemas.CtsTransactions.Entities;

public partial class CtAnimalChange
{
    public long TransId { get; set; }

    public string TransType { get; set; } = null!;

    public decimal AchId { get; set; }

    public string? AchCurrentStatus { get; set; }

    public string? AchCurrentUser { get; set; }

    public DateOnly? AchCurrentModifiedDate { get; set; }

    public decimal? AchCurrentPid { get; set; }

    public decimal? AchRanIdDocIssued { get; set; }

    public decimal? AchLocIdDocIssued { get; set; }

    public DateOnly? AchDocIssuedDate { get; set; }

    public string? AchPassportVersionNumber { get; set; }

    public decimal? AchMovIdDeathCancel { get; set; }

    public string? AchBreedOriginal { get; set; }

    public string? AchBreedNew { get; set; }

    public string? AchSexOriginal { get; set; }

    public string? AchSexNew { get; set; }

    public DateOnly? AchBirthDateOriginal { get; set; }

    public DateOnly? AchBirthDateNew { get; set; }

    public string? AchEartagOriginal { get; set; }

    public string? AchEartagNew { get; set; }

    public decimal? RowNumber { get; set; }

    public long? AchAudId { get; set; }

    public string? AchAudType { get; set; }

    public DateTime? AchAudDatetime { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? CtsFileImportId { get; set; }
}
