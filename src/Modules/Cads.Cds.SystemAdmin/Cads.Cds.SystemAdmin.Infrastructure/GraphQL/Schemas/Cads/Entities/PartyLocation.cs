using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Schemas.Cads.Entities;

public partial class PartyLocation
{
    public string Identifier { get; set; } = null!;

    public virtual ICollection<Party> Parties { get; set; } = new List<Party>();
}