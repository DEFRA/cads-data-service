using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class LocationSiteRole
{
    public string Role { get; set; } = null!;

    public virtual ICollection<LocationSiteParty> LocationSiteParties { get; set; } = new List<LocationSiteParty>();
}