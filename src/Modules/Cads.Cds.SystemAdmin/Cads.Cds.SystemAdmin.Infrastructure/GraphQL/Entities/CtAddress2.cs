using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtAddress2
{
    public long AuditId { get; set; }

    public string AuditAction { get; set; } = null!;

    public long? AuditTransId { get; set; }

    public DateTime AuditedAt { get; set; }

    public decimal AdrId { get; set; }

    public decimal? AdrLocId { get; set; }

    public decimal? AdrParId { get; set; }

    public string? AdrName { get; set; }

    public string? AdrAddress2 { get; set; }

    public string? AdrAddress3 { get; set; }

    public string? AdrAddress4 { get; set; }

    public string? AdrAddress5 { get; set; }

    public string? AdrPostCode { get; set; }

    public DateOnly? AdrCurrentModifiedDate { get; set; }

    public string? AdrCurrentStatus { get; set; }

    public string? AdrCurrentUser { get; set; }

    public decimal? AdrCurrentPid { get; set; }

    public decimal? AdrVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public long? TransId { get; set; }

    public virtual CtAddress1? AuditTrans { get; set; }
}