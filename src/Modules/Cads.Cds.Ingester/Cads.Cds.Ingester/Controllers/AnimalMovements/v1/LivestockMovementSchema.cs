using System.Text.Json.Serialization;

namespace Cads.Cds.Ingester.Controllers.AnimalMovements.v1;

// Generated from the AnimalMovement JSON schema (v1.3, 2023-02-24).
// Requires .NET 9+ for [JsonStringEnumMemberName].
//
// Notes:
// - Dates and timestamps are kept as strings on purpose. The schema allows empty strings
//   (ArrivalDate, LoadingTimestamp, UnloadingTimestamp) and timestamps without seconds,
//   which DateOnly/DateTimeOffset would reject.
// - `required` members mirror the schema's "required" arrays. Everything else is nullable.

/// <summary>Root of the message.</summary>
public sealed class AnimalMovementMessage
{
    public required AnimalMovement AnimalMovement { get; init; }
}

public sealed class AnimalMovement
{
    /// <summary>The system that created the movement.</summary>
    public required Region CreatedBy { get; init; }

    /// <summary>The region the animal started its journey from.</summary>
    public required Region DepartureRegion { get; init; }

    /// <summary>The region the animal arrived at - its destination.</summary>
    public required Region DestinationRegion { get; init; }

    /// <summary>A unique identifier for the transaction.</summary>
    public required string MessageID { get; init; }

    /// <summary>Format YYYY-MM-DDThh:mm:ssZ (UTC).</summary>
    public required string MovementTime { get; init; }

    /// <summary>Known as the SupplierRefID for EID Cymru.</summary>
    public required string MovementID { get; init; }

    /// <summary>Grouping identifier under which several movements may exist (UMIDUID within EID Cymru).</summary>
    public string? MovementGroupID { get; init; }

    /// <summary>Movement grouping under which multiple movements may exist (UMID within EID Cymru).</summary>
    public string? MovementGroup { get; init; }

    /// <summary>Identifier enabling pairing of cross-border movements.</summary>
    public string? CrossBorderMovementId { get; init; }

    public required MovementType MovementType { get; init; }

    public required ActionType ActionType { get; init; }

    public required ActionStatus ActionStatus { get; init; }

    /// <summary>Schema default is Receiver.</summary>
    public required SupplierType SupplierType { get; init; }

    public required AnimalType AnimalType { get; init; }

    public MovementDetails? MovementDetails { get; init; }

    public List<AnimalGroupDetail>? AnimalGroupDetails { get; init; }

    public List<AnimalDetail>? AnimalDetails { get; init; }
}

public sealed class MovementDetails
{
    /// <summary>Format YYYY-MM-DD.</summary>
    public required string DepartureDate { get; init; }

    public required string DepartureLocation { get; init; }

    public string? DepartureCoreID { get; init; }

    public string? DepartureLocationType { get; init; }

    public string? DeparturePremisesActivity { get; init; }

    /// <summary>Schema default is "0".</summary>
    public string? OffExemptionCode { get; init; } = "0";

    /// <summary>Optional. Format YYYY-MM-DD, or empty string.</summary>
    public string? ArrivalDate { get; init; }

    public required string DestinationLocation { get; init; }

    public string? DestinationCoreID { get; init; }

    public string? DestinationLocationType { get; init; }

    public string? DestinationPremisesActivity { get; init; }

    /// <summary>Schema default is "0".</summary>
    public string? OnExemptionCode { get; init; } = "0";

    public string? LotNumber { get; init; }

    /// <summary>Total number of animals within this movement transaction.</summary>
    public required int NumberOfAnimals { get; init; }

    /// <summary>Total number of animals DOA within this movement transaction.</summary>
    public required int NumberOfDOAs { get; init; }

    /// <summary>Total number of animals where the eartag has been electronically read.</summary>
    public required int NumberOfReads { get; init; }

    /// <summary>
    /// Of first animal loaded. YYYY-MM-DDThh:mm:ssZ or YYYY-MM-DDThh:mmZ, or empty string.
    /// </summary>
    public string? LoadingTimestamp { get; init; }

    public string? ExpectedJourneyDuration { get; init; }

    /// <summary>
    /// Datetime last animal unloaded. YYYY-MM-DDThh:mm:ssZ or YYYY-MM-DDThh:mmZ, or empty string.
    /// </summary>
    public string? UnloadingTimestamp { get; init; }

    public AnimalTransporter? AnimalTransporter { get; init; }

    public HaulierDetails? HaulierDetails { get; init; }
}

public sealed class HaulierDetails
{
    public string? HaulierID { get; init; }

    public string? Name { get; init; }

    public string? AddressLine1 { get; init; }

    public string? AddressLine2 { get; init; }

    public string? AddressLine3 { get; init; }

    public string? AddressLine4 { get; init; }

    public string? Postcode { get; init; }

    public string? Telephone { get; init; }

    public string? Email { get; init; }

    public int? TotalVehicles { get; init; }

    public List<VehicleDetail>? VehicleDetails { get; init; }
}

public sealed class VehicleDetail
{
    public string? VehicleRegistrationNumber { get; init; }

    public string? DriverName { get; init; }

    public string? TrailerIdentificationNumber { get; init; }

    public string? TransportAuthorisationNumber { get; init; }
}

public sealed class AnimalGroupDetail
{
    /// <summary>e.g. FlockNumber.</summary>
    public required string AnimalGroupNumber { get; init; }

    public required AnimalGroupType AnimalGroupType { get; init; }

    public required int NumberOfAnimals { get; init; }

    public required int NumberOfDOAs { get; init; }
}

public sealed class AnimalDetail
{
    /// <summary>i.e. EartagID.</summary>
    public required string AnimalID { get; init; }

    public required AnimalIdType AnimalIDType { get; init; }

    public string? AnimalIDTransponderHex { get; init; }

    public string? AnimalIDTransponderISO { get; init; }

    /// <summary>The issue number for the tag - re-tag counter.</summary>
    public string? TagIssueNumber { get; init; }

    public bool? DOA { get; init; }

    public string? Breed { get; init; }

    public Gender? Gender { get; init; }

    /// <summary>Format YYYY-MM-DD.</summary>
    public string? DOB { get; init; }

    public string? DamID { get; init; }

    public string? SurrogateDamID { get; init; }

    public string? SireID { get; init; }

    /// <summary>Format YYYY-MM-DD.</summary>
    public string? DateOfDeath { get; init; }

    /// <summary>
    /// Captured against individual animal - applied to APHA for a licence in order to
    /// move an animal which would not be able to move otherwise.
    /// </summary>
    public string? LicenceType { get; init; }

    public List<AnimalGroup>? AnimalGroups { get; init; }

    public PassportDetails? PassportDetails { get; init; }
}

public sealed class AnimalGroup
{
    /// <summary>e.g. FlockMark, Lot Number.</summary>
    public required string AnimalGroupNumber { get; init; }

    public required AnimalGroupMarkType AnimalGroupType { get; init; }
}

public sealed class PassportDetails
{
    public DocumentType? DocumentType { get; init; }

    /// <summary>Format YYYY-MM-DD.</summary>
    public string? DateOfRegistration { get; init; }

    public string? PassportStatusCode { get; init; }

    public int? PassportVersion { get; init; }

    public bool? RefusedPassport { get; init; }
}

// ---------------------------------------------------------------------------
// Enums
// ---------------------------------------------------------------------------

[JsonConverter(typeof(JsonStringEnumConverter<Region>))]
public enum Region
{
    [JsonStringEnumMemberName("WA")] Wales,
    [JsonStringEnumMemberName("EN")] England,
    [JsonStringEnumMemberName("SC")] Scotland,
    [JsonStringEnumMemberName("NI")] NorthernIreland
}

/// <summary>01 - keeper (or The Bureau for EID Cymru); 02 - market; 03 - abattoir.</summary>
[JsonConverter(typeof(JsonStringEnumConverter<MovementType>))]
public enum MovementType
{
    [JsonStringEnumMemberName("01")] Keeper,
    [JsonStringEnumMemberName("02")] Market,
    [JsonStringEnumMemberName("03")] Abattoir
}

[JsonConverter(typeof(JsonStringEnumConverter<ActionType>))]
public enum ActionType
{
    New,
    Amend,
    Delete
}

[JsonConverter(typeof(JsonStringEnumConverter<ActionStatus>))]
public enum ActionStatus
{
    NeedsAcceptance,
    Queried,
    Completed,
    Recorded,
    Deleted
}

[JsonConverter(typeof(JsonStringEnumConverter<SupplierType>))]
public enum SupplierType
{
    Dispatcher,
    Receiver
}

[JsonConverter(typeof(JsonStringEnumConverter<AnimalType>))]
public enum AnimalType
{
    Sheep,
    Cattle,
    Goats,
    Deer,
    Pigs
}

/// <summary>01=DepartureKeeper; 02=ReceivingKeeper; 03=Haulier.</summary>
[JsonConverter(typeof(JsonStringEnumConverter<AnimalTransporter>))]
public enum AnimalTransporter
{
    [JsonStringEnumMemberName("01")] DepartureKeeper,
    [JsonStringEnumMemberName("02")] ReceivingKeeper,
    [JsonStringEnumMemberName("03")] Haulier
}

/// <summary>
/// Used by AnimalGroupDetail. Flock Mark (Sheep); Herd Mark (Cows); Individual Identification Mark (Pigs);
/// Unknown Permanent Mark (Sheep, Goats, Pigs, Deer); Tattoo Number (Pigs).
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<AnimalGroupType>))]
public enum AnimalGroupType
{
    [JsonStringEnumMemberName("FM")] FlockMark,
    [JsonStringEnumMemberName("HM")] HerdMark,
    [JsonStringEnumMemberName("IM")] IndividualIdentificationMark,
    [JsonStringEnumMemberName("PU")] UnknownPermanentMark,
    [JsonStringEnumMemberName("TN")] TattooNumber
}

/// <summary>
/// Used by AnimalDetail.AnimalIDType. Same codes as <see cref="AnimalGroupType"/>, kept separate
/// because the schema defines them independently and they could diverge.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<AnimalIdType>))]
public enum AnimalIdType
{
    [JsonStringEnumMemberName("FM")] FlockMark,
    [JsonStringEnumMemberName("HM")] HerdMark,
    [JsonStringEnumMemberName("IM")] IndividualIdentificationMark,
    [JsonStringEnumMemberName("PU")] UnknownPermanentMark,
    [JsonStringEnumMemberName("TN")] TattooNumber
}

/// <summary>Used by AnimalDetail.AnimalGroups. Flock Mark, Herd Mark, Lot Number.</summary>
[JsonConverter(typeof(JsonStringEnumConverter<AnimalGroupMarkType>))]
public enum AnimalGroupMarkType
{
    [JsonStringEnumMemberName("FM")] FlockMark,
    [JsonStringEnumMemberName("HM")] HerdMark,
    [JsonStringEnumMemberName("LT")] LotNumber
}

[JsonConverter(typeof(JsonStringEnumConverter<Gender>))]
public enum Gender
{
    Female,
    Male
}

[JsonConverter(typeof(JsonStringEnumConverter<DocumentType>))]
public enum DocumentType
{
    Passport,
    CPP35
}
