using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtsFileImportStatus
{
    public short ImportStatusId { get; set; }

    public string StatusDescription { get; set; } = null!;

    public virtual ICollection<CtsFileImport> CtsFileImports { get; set; } = new List<CtsFileImport>();
}