using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtMsgtxt
{
    public string? MsgId { get; set; }

    public string? MsgText { get; set; }

    public decimal? RowNumber { get; set; }

    public long? TransId { get; set; }
}
