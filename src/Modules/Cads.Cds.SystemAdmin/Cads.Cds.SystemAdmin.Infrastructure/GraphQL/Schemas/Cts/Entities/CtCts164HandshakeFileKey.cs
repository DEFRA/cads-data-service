using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Schemas.Cts.Entities;

public partial class CtCts164HandshakeFileKey
{
    public decimal? BjkBatchId { get; set; }

    public decimal? BjkGroupId { get; set; }

    public string? BjkFiletype { get; set; }

    public string? BjkKey { get; set; }

    public DateOnly? BjkModifiedDate { get; set; }

    public long? TransId { get; set; }
}
