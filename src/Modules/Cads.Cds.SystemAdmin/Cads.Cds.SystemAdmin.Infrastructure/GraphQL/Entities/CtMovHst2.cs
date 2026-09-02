using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtMovHst2
{
    public long AuditId { get; set; }

    public string AuditAction { get; set; } = null!;

    public long? AuditTransId { get; set; }

    public DateTime AuditedAt { get; set; }

    public DateOnly? HstOndate { get; set; }

    public decimal? HstOntype { get; set; }

    public string? HstOnsource { get; set; }

    public decimal? HstOffkey { get; set; }

    public DateOnly? HstOffdate { get; set; }

    public decimal? HstOfftype { get; set; }

    public string? HstOffsource { get; set; }

    public string? HstPairind { get; set; }

    public string? HstSplitflg { get; set; }

    public decimal? HstKey { get; set; }

    public decimal? HstLkey { get; set; }

    public decimal? HstOnkey { get; set; }

    public decimal? RowNumber { get; set; }

    public long? TransId { get; set; }

    public virtual CtMovHst1? AuditTrans { get; set; }
}
