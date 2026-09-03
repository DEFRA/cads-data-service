using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtCommsAddress
{
    public decimal CoaId { get; set; }

    public string? CoaCurrentStatus { get; set; }

    public string? CoaCurrentUser { get; set; }

    public DateOnly? CoaCurrentModifiedDate { get; set; }

    public decimal? CoaPid { get; set; }

    public string? CoaEmailAddress { get; set; }

    public string? CoaAttachment { get; set; }

    public decimal? RowNumber { get; set; }

    public long? TransId { get; set; }
}
