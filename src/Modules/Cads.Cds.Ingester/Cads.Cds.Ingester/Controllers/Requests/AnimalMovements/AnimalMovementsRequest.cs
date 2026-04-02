using System.Text.Json.Serialization;

namespace Cads.Cds.Ingester.Controllers.Requests.AnimalMovements;

public class AnimalMovementsRequest
{
    [JsonPropertyName("AnimalMovement")]
    public AnimalMovement? AnimalMovement { get; set; }
}