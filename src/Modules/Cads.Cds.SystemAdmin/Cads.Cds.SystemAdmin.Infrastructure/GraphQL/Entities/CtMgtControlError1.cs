using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtMgtControlError1
{
    public long TransId { get; set; }

    public string TransType { get; set; } = null!;

    public decimal MceId { get; set; }

    public decimal? MceRanId { get; set; }

    public string? MceErrorCode { get; set; }

    public decimal? McePassportVersionIssued { get; set; }

    public decimal? MceNumberOfDaysLate { get; set; }

    public string? MceCurrentUser { get; set; }

    public string? MceCurrentStatus { get; set; }

    public DateOnly? MceCurrentModifiedDate { get; set; }

    public decimal? MceCurrentPid { get; set; }

    public decimal? MceVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public long? MceAudId { get; set; }

    public string? MceAudType { get; set; }

    public DateTime? MceAudDatetime { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? CtsFileImportId { get; set; }

    public virtual ICollection<CtMgtControlError2> CtMgtControlError2s { get; set; } = new List<CtMgtControlError2>();

    public virtual CtsFileImport? CtsFileImport { get; set; }
}