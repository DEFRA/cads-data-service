using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class MiReportGroup
{
    public Guid GroupId { get; set; }

    public string GroupKey { get; set; } = null!;

    public string Title { get; set; } = null!;

    public int DisplayOrder { get; set; }

    public virtual ICollection<MiReport> Reports { get; set; } = new List<MiReport>();
}