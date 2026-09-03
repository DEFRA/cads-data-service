using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtEmailLog
{
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

    public long? TransId { get; set; }
}