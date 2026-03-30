using System.Text.Json.Serialization;

namespace Cads.Cds.Ingester.Controllers.Requests.AnimalMovements;

public class AnimalMovement
{
    [JsonPropertyName("CreatedBy")]
    public string? CreatedBy { get; set; }

    [JsonPropertyName("DepartureRegion")]
    public string? DepartureRegion { get; set; }

    [JsonPropertyName("DestinationRegion")]
    public string? DestinationRegion { get; set; }

    [JsonPropertyName("MessageID")]
    public string? MessageID { get; set; }

    [JsonPropertyName("MovementTime")]
    public DateTime? MovementTime { get; set; }

    [JsonPropertyName("MovementID")]
    public string? MovementID { get; set; }

    [JsonPropertyName("MovementGroupID")]
    public string? MovementGroupID { get; set; }

    [JsonPropertyName("MovementGroup")]
    public string? MovementGroup { get; set; }

    [JsonPropertyName("CrossBorderMovementId")]
    public string? CrossBorderMovementId { get; set; }

    [JsonPropertyName("MovementType")]
    public string? MovementType { get; set; }

    [JsonPropertyName("ActionType")]
    public string? ActionType { get; set; }

    [JsonPropertyName("ActionStatus")]
    public string? ActionStatus { get; set; }

    [JsonPropertyName("SupplierType")]
    public string? SupplierType { get; set; }

    [JsonPropertyName("AnimalType")]
    public string? AnimalType { get; set; }

    [JsonPropertyName("MovementDetails")]
    public MovementDetails? MovementDetails { get; set; }

    [JsonPropertyName("AnimalGroupDetails")]
    public List<AnimalGroupDetail>? AnimalGroupDetails { get; set; }

    [JsonPropertyName("AnimalDetails")]
    public List<AnimalDetail>? AnimalDetails { get; set; }
}