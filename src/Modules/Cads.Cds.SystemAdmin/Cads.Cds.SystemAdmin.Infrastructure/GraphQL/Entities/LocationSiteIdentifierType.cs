using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class LocationSiteIdentifierType
{
    public string Type { get; set; } = null!;

    public virtual ICollection<LocationSiteIdentifier> LocationSiteIdentifiers { get; set; } = new List<LocationSiteIdentifier>();
}