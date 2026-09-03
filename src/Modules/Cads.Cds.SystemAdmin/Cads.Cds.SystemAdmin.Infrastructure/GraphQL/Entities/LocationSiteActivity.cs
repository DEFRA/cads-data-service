using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class LocationSiteActivity
{
    public string SiteIdentifier { get; set; } = null!;

    public string SiteType { get; set; } = null!;

    public string Activity { get; set; } = null!;

    public DateOnly StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public virtual LocationSiteTypeActivity LocationSiteTypeActivity { get; set; } = null!;

    public virtual LocationSite SiteIdentifierNavigation { get; set; } = null!;
}