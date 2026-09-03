using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class PartyState
{
    public string State { get; set; } = null!;

    public virtual ICollection<Party> Parties { get; set; } = new List<Party>();
}