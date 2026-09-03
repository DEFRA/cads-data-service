using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class LocationPostcode
{
    public string Postcode { get; set; } = null!;

    public virtual ICollection<Location> Locations { get; set; } = new List<Location>();
}