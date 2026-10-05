using Microsoft.AspNetCore.Mvc;

namespace Cads.Cds.Api.Controllers.Requests.Bovine;

public class GetAnimalsOnHoldingRequest
{
    [FromQuery(Name = "CPH")]
    public required string Cph { get; init; }
}