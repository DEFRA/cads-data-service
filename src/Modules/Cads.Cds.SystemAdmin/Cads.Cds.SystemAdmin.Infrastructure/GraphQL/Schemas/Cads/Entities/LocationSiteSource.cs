using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Schemas.Cads.Entities;

public partial class LocationSiteSource
{
    public string Source { get; set; } = null!;

    public virtual ICollection<LocationSite> LocationSites { get; set; } = new List<LocationSite>();
}
