using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class LocationAssociatedSiteType
{
    public string Type { get; set; } = null!;

    public virtual ICollection<LocationAssociatedSite> LocationAssociatedSites { get; set; } = new List<LocationAssociatedSite>();
}
