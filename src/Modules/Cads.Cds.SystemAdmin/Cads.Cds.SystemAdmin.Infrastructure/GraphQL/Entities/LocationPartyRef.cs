using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class LocationPartyRef
{
    public long PartyIdentifier { get; set; }

    public virtual ICollection<LocationSiteParty> LocationSiteParties { get; set; } = new List<LocationSiteParty>();
}
