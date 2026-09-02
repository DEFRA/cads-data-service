using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class DataSeedIngestionHistory
{
    public long Id { get; set; }

    public string FileName { get; set; } = null!;

    public DateTime AppliedAt { get; set; }

    public string Checksum { get; set; } = null!;
}
