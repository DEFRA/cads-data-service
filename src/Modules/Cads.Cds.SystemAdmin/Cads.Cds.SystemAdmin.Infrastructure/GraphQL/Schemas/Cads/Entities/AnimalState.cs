using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Schemas.Cads.Entities;

public partial class AnimalState
{
    public string State { get; set; } = null!;

    public virtual ICollection<AnimalStatus> AnimalStatuses { get; set; } = new List<AnimalStatus>();
}
