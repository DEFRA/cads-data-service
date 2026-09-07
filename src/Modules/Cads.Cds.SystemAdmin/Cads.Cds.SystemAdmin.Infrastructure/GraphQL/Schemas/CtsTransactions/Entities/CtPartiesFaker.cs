using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Schemas.CtsTransactions.Entities;

public partial class CtPartiesFaker
{
    public long TransId { get; set; }

    public string TransType { get; set; } = null!;

    public string? ParSurname { get; set; }

    public string? ParInitials { get; set; }

    public string? ParTitle { get; set; }

    public string? ParTelNumber { get; set; }

    public string? ParMobileNumber { get; set; }

    public string? ParEmailAddress { get; set; }

    public long? ParAudId { get; set; }

    public string? ParAudType { get; set; }

    public DateTime? ParAudDatetime { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? CtsFileImportId { get; set; }
}
