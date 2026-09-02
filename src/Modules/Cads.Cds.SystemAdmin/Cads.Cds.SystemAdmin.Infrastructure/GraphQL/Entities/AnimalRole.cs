using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class AnimalRole
{
    public string Role { get; set; } = null!;

    public virtual ICollection<AnimalParty> AnimalParties { get; set; } = new List<AnimalParty>();
}
