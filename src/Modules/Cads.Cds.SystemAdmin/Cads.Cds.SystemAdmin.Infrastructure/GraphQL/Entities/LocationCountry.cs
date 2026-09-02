using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class LocationCountry
{
    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public bool EuropeanUnionTradeMemberFlag { get; set; }

    public bool DevolvedAuthorityFlag { get; set; }

    public bool HomeCountryFlag { get; set; }

    public virtual ICollection<Location> Locations { get; set; } = new List<Location>();
}
