using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class Party
{
    public int Number { get; set; }

    public string PartyType { get; set; } = null!;

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string Name { get; set; } = null!;

    public string? Mobile { get; set; }

    public string? Landline { get; set; }

    public string? Email { get; set; }

    public string? LocationIdentifier { get; set; }

    public string PartyState { get; set; } = null!;

    public virtual PartyLocation? LocationIdentifierNavigation { get; set; }

    public virtual ICollection<PartyHaulier> PartyHauliers { get; set; } = new List<PartyHaulier>();

    public virtual PartyState PartyStateNavigation { get; set; } = null!;

    public virtual PartyType PartyTypeNavigation { get; set; } = null!;
}
