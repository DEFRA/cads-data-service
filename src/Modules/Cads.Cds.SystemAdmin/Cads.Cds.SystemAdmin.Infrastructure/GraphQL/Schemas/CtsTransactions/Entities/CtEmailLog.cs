using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Schemas.CtsTransactions.Entities;

public partial class CtEmailLog
{
    public long TransId { get; set; }

    public string TransType { get; set; } = null!;

    public decimal EmlId { get; set; }

    public DateOnly? EmlSentDatetime { get; set; }

    public string? EmlEmailAddrRecd { get; set; }

    public string? EmlFileName { get; set; }

    public DateOnly? EmlReceivedDatetime { get; set; }

    public string? EmlSendReturnCode { get; set; }

    public string? EmlEmailAddrSent { get; set; }

    public string? EmlCurrentUser { get; set; }

    public DateOnly? EmlCurrentModifiedDate { get; set; }

    public decimal? EmlCurrentPid { get; set; }

    public string? EmlCurrentStatus { get; set; }

    public decimal? EmlVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public long? EmlAudId { get; set; }

    public string? EmlAudType { get; set; }

    public DateTime? EmlAudDatetime { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? CtsFileImportId { get; set; }
}