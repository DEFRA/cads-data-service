using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Schemas.Cads.Entities;

public partial class LocationSiteState
{
    public string State { get; set; } = null!;

    public virtual ICollection<LocationSite> LocationSites { get; set; } = new List<LocationSite>();
}
