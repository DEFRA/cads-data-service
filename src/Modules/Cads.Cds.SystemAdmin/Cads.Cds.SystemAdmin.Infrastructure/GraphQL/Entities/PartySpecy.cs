using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class PartySpecy
{
    public string Species { get; set; } = null!;

    public virtual ICollection<PartyHaulier> HaulierIdentifiers { get; set; } = new List<PartyHaulier>();
}
