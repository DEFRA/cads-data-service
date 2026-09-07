using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Schemas.CtsTransactions.Entities;

public partial class CtCtsUser
{
    public long TransId { get; set; }

    public string TransType { get; set; } = null!;

    public decimal CusId { get; set; }

    public string? CusUserIdentifier { get; set; }

    public string? CusColonFlag { get; set; }

    public string? CusGrade { get; set; }

    public string? CusTeamReference { get; set; }

    public string? CusAccessGroup { get; set; }

    public string? CusRoomName { get; set; }

    public string? CusEmailAddress { get; set; }

    public decimal? CusVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public long? CusAudId { get; set; }

    public string? CusAudType { get; set; }

    public DateTime? CusAudDatetime { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? CtsFileImportId { get; set; }
}
