using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtCountry1
{
    public decimal CryId { get; set; }

    public string? CryCode { get; set; }

    public string? CryName { get; set; }

    public string? CryEuMember { get; set; }

    public string? CryImportExport { get; set; }

    public decimal? CryCryIdMainEu { get; set; }

    public string? CryBackCapture { get; set; }

    public string? CryCurrentUser { get; set; }

    public string? CryCurrentStatus { get; set; }

    public DateOnly? CryCurrentModifiedDate { get; set; }

    public decimal? CryCurrentPid { get; set; }

    public decimal? CryVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public long TransId { get; set; }

    public string TransType { get; set; } = null!;

    public long? CryAudId { get; set; }

    public string? CryAudType { get; set; }

    public DateTime? CryAudDatetime { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? CtsFileImportId { get; set; }

    public virtual CtsFileImport? CtsFileImport { get; set; }
}
