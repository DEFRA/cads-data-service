using Microsoft.AspNetCore.Mvc;

namespace Cads.Cds.MiBff.Controllers.Requests;

public class GetAnimalsRequest
{
    [FromRoute] public string AnimalId { get; set; } = string.Empty;
}