using System.Text.Json.Serialization;

namespace Cads.Cds.Ingester.Controllers.Requests.AnimalMovements;

public class VehicleDetail
{
    [JsonPropertyName("VehicleRegistrationNumber")]
    public string? VehicleRegistrationNumber { get; set; }

    [JsonPropertyName("DriverName")]
    public string? DriverName { get; set; }

    [JsonPropertyName("TransportAuthorisationNumber")]
    public string? TransportAuthorisationNumber { get; set; }
}