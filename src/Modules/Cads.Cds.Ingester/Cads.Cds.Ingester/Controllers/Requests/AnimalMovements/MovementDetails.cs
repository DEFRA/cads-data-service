using System.Text.Json.Serialization;

namespace Cads.Cds.Ingester.Controllers.Requests.AnimalMovements;

public class MovementDetails
{
    [JsonPropertyName("DepartureDate")]
    public DateTime DepartureDate { get; set; }

    [JsonPropertyName("DepartureLocation")]
    public string? DepartureLocation { get; set; }

    [JsonPropertyName("DepartureCoreID")]
    public string? DepartureCoreID { get; set; }

    [JsonPropertyName("DepartureLocationType")]
    public string? DepartureLocationType { get; set; }

    [JsonPropertyName("DeparturePremisesActivity")]
    public string? DeparturePremisesActivity { get; set; }

    [JsonPropertyName("OffExemptionCode")]
    public string? OffExemptionCode { get; set; }

    [JsonPropertyName("ArrivalDate")]
    public DateOnly ArrivalDate { get; set; }

    [JsonPropertyName("DestinationLocation")]
    public string? DestinationLocation { get; set; }

    [JsonPropertyName("DestinationCoreID")]
    public string? DestinationCoreID { get; set; }

    [JsonPropertyName("DestinationLocationType")]
    public string? DestinationLocationType { get; set; }

    [JsonPropertyName("DestinationPremisesActivity")]
    public string? DestinationPremisesActivity { get; set; }

    [JsonPropertyName("OnExemptionCode")]
    public string? OnExemptionCode { get; set; }

    [JsonPropertyName("LotNumber")]
    public string? LotNumber { get; set; }

    [JsonPropertyName("NumberOfAnimals")]
    public int NumberOfAnimals { get; set; }

    [JsonPropertyName("NumberOfDOAs")]
    public int NumberOfDOAs { get; set; }

    [JsonPropertyName("NumberOfReads")]
    public int NumberOfReads { get; set; }

    [JsonPropertyName("LoadingTimestamp")]
    public string? LoadingTimestamp { get; set; }

    [JsonPropertyName("ExpectedJourneyDuration")]
    public string? ExpectedJourneyDuration { get; set; }

    [JsonPropertyName("UnloadingTimestamp")]
    public string? UnloadingTimestamp { get; set; }

    [JsonPropertyName("AnimalTransporter")]
    public string? AnimalTransporter { get; set; }

    [JsonPropertyName("HaulierDetails")]
    public HaulierDetails? HaulierDetails { get; set; }
}