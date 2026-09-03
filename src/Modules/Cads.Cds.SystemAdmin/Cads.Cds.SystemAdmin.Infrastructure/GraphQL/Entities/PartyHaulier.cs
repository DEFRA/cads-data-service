using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class PartyHaulier
{
    public string Identifier { get; set; } = null!;

    public int PartyNumber { get; set; }

    public string AuthorisationNumber { get; set; } = null!;

    public DateOnly StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public virtual Party PartyNumberNavigation { get; set; } = null!;

    public virtual ICollection<PartySpecy> Species { get; set; } = new List<PartySpecy>();
}