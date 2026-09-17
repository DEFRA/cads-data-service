using Cads.Cds.Api.Core.Domain.Bovine;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace Cads.Cds.Api.Controllers.Requests.Bovine;

public class GetAnimalsOnHoldingRequest
{
    [FromQuery(Name = "CPH")]
    public required string Cph { get; init; }

    [FromQuery(Name = "holdingAssociation")]
    [DefaultValue(HoldingAssociation.MovedOnHolding)]
    public HoldingAssociation HoldingAssociation { get; init; } = HoldingAssociation.MovedOnHolding;
}