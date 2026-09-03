using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class AnimalCollectiveState
{
    public string State { get; set; } = null!;

    public virtual ICollection<AnimalCollectiveRef> AnimalCollectiveRefs { get; set; } = new List<AnimalCollectiveRef>();
}