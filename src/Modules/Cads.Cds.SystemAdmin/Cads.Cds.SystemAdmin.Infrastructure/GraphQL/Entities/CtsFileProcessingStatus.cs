using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtsFileProcessingStatus
{
    public short ProcessingStatusId { get; set; }

    public string StatusDescription { get; set; } = null!;

    public virtual ICollection<CtsFileImport> CtsFileImports { get; set; } = new List<CtsFileImport>();
}
