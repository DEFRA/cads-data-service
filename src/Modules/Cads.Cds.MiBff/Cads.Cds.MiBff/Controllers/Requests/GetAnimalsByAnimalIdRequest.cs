using Microsoft.AspNetCore.Mvc;

namespace Cads.Cds.MiBff.Controllers.Requests;

public class GetAnimalsByAnimalIdRequest 
{
    [FromRoute] public required Guid AnimalId { get; set; }
}