using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class PartyType
{
    public string Type { get; set; } = null!;

    public virtual ICollection<Party> Parties { get; set; } = new List<Party>();
}