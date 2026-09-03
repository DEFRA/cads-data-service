using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class LocationSiteType
{
    public string Type { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<LocationSiteTypeActivity> LocationSiteTypeActivities { get; set; } = new List<LocationSiteTypeActivity>();

    public virtual ICollection<LocationSite> LocationSites { get; set; } = new List<LocationSite>();
}