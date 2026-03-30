using System.Text.Json.Serialization;

namespace Cads.Cds.Ingester.Controllers.Requests.AnimalMovements;

public class HaulierDetails
{
    [JsonPropertyName("Name")]
    public string? Name { get; set; }

    [JsonPropertyName("Telephone")]
    public string? Telephone { get; set; }

    [JsonPropertyName("Email")]
    public string? Email { get; set; }

    [JsonPropertyName("VehicleDetails")]
    public List<VehicleDetail>? VehicleDetails { get; set; }
}