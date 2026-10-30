using System.Text.Json.Serialization;

namespace Cads.Cds.Ingester.Controllers.AnimalMovements.v2;

// Generated from the Event JSON schema (v2).
// Requires .NET 9+ for [JsonStringEnumMemberName].
//
// Notes:
// - Dates and timestamps are kept as strings on purpose. The schema allows empty strings
//   (ArrivalDate, LoadingTimestamp, UnloadingTimestamp) and timestamps without seconds,
//   which DateOnly/DateTimeOffset would reject.
// - `required` members mirror the schema's "required" arrays. Everything else is nullable.
// - The V2 namespace lets this sit alongside the v1 classes without type name clashes.

/// <summary>Root of the message.</summary>
public sealed class EventMessage
{
    public required LivestockEvent Event { get; init; }
}

public sealed class LivestockEvent
{
    /// <summary>Unique identifier for the record.</summary>
    public required string RecordID { get; init; }

    /// <summary>Unique identifier for the event - birth, death, movement, etc.</summary>
    public required string EventID { get; init; }

    public required EventType EventType { get; init; }

    public required ActionType ActionType { get; init; }

    /// <summary>The Devolved Administration livestock tracing system the data has been sourced from.</summary>
    public required Region CreatedBy { get; init; }

    /// <summary>
    /// UTC date and time the record was created on the source system. Format YYYY-MM-DDThh:mm:ssZ.
    /// </summary>
    public required string CreatedDate { get; init; }

    /// <summary>Species of the animal.</summary>
    public Species? Species { get; init; }

    public AnimalMovement? AnimalMovement { get; init; }

    public List<AnimalGroupDetail>? AnimalGroupDetails { get; init; }

    public List<AnimalDetail>? AnimalDetails { get; init; }

    public HoldingDetails? HoldingDetails { get; init; }
}

public sealed class AnimalMovement
{
    /// <summary>The territory from which the movement originated.</summary>
    public Region? DepartureRegion { get; init; }

    /// <summary>A movement grouping under which multiple movements may exist - i.e. UMID within EID Cymru.</summary>
    public string? MovementGroup { get; init; }

    public required MovementType MovementType { get; init; }

    /// <summary>Status of the move.</summary>
    public MovementStatus? MovementStatus { get; init; }

    /// <summary>Schema default is Receiver.</summary>
    public required SupplierType SupplierType { get; init; }

    /// <summary>Format YYYY-MM-DD.</summary>
    public string? DepartureDate { get; init; }

    public string? DepartureLocation { get; init; }

    public string? DepartureCoreID { get; init; }

    public string? DepartureLocationType { get; init; }

    public string? DeparturePremisesActivity { get; init; }

    /// <summary>Format YYYY-MM-DD, or empty string.</summary>
    public string? ArrivalDate { get; init; }

    public string? DestinationLocation { get; init; }

    public string? DestinationCoreID { get; init; }

    public string? DestinationLocationType { get; init; }

    public string? DestinationPremisesActivity { get; init; }

    /// <summary>Total number of animals within this movement transaction.</summary>
    public required int NumberOfAnimals { get; init; }

    /// <summary>Total number of animals DOA within this movement transaction.</summary>
    public int? NumberOfDOAs { get; init; }

    /// <summary>Total number of animals where the eartag has been electronically read.</summary>
    public int? NumberOfReads { get; init; }

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

    public string? LotNumber { get; init; }

    /// <summary>The MovementID for the alternate country where a move takes place across a border.</summary>
    public string? CrossBorderMovementID { get; init; }

    /// <summary>The region the animal arrived at - its destination.</summary>
    public Region? DestinationRegion { get; init; }

    /// <summary>The system that first generated the movement.</summary>
    public Region? CrossBorderSource { get; init; }

    public required MovementDirection MovementDirection { get; init; }

    public YesNo? WithinBusinessMovement { get; init; }

    /// <summary>The main CPH which acts as the parent for a within business group of CPHs.</summary>
    public string? WithinBusinessMainCPH { get; init; }

    /// <summary>
    /// CPH of initial location of animals (where move has been ported onto the main CPH
    /// for a group of within business holdings).
    /// </summary>
    public string? InitialWithinBusinessLocation { get; init; }

    public YesNo? EmergencySlaughter { get; init; }
}

public sealed class HaulierDetails
{
    public required string HaulierID { get; init; }

    public string? HaulierCompanyName { get; init; }

    public string? HaulierAddressLine1 { get; init; }

    public string? HaulierAddressLine2 { get; init; }

    public string? HaulierAddressLine3 { get; init; }

    public string? HaulierAddressLine4 { get; init; }

    public string? HaulierPostcode { get; init; }

    public string? HaulierTelephone { get; init; }

    public string? HaulierEmail { get; init; }

    public int? TotalVehicles { get; init; }

    public List<VehicleDetail>? VehicleDetails { get; init; }
}

public sealed class VehicleDetail
{
    // The schema lists VehicleRegistrationNumber as required, but places that "required" on the
    // array rather than the item, so JSON Schema ignores it. Kept nullable to match what the
    // schema actually enforces. Make it `required` if the intent was to enforce it.
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

    public required bool DOA { get; init; }

    public string? Breed { get; init; }

    public Gender? Gender { get; init; }

    /// <summary>Format YYYY-MM-DD.</summary>
    public string? DOB { get; init; }

    /// <summary>Format YYYY-MM-DD.</summary>
    public string? DateBirthReported { get; init; }

    public string? BirthLocation { get; init; }

    public string? GeneticDamID { get; init; }

    public string? SurrogateDamID { get; init; }

    public string? SireID { get; init; }

    /// <summary>
    /// How an animal has arrived in GB; may be by birth, registration (if late registering the birth) or import.
    /// </summary>
    public string? RegistrationType { get; init; }

    /// <summary>Passport, NOR (CPP35), CPS Passport, Certificate of Registration.</summary>
    public string? RegistrationDocumentType { get; init; }

    /// <summary>Location of animal registration (Holding No.); same as BirthLocation when the event is a birth.</summary>
    public string? RegistrationLocation { get; init; }

    /// <summary>
    /// NOR - Late Reg; NOR - Incomplete; NOR - Passport Revoked; Passport Issued; NOR - Passport Refused;
    /// In suspense; NOR NTI (Notice to identify); NOR-V Restricted at inspection; NOR-D Registered Dead;
    /// NORA - Found at inspection.
    /// </summary>
    public string? RegistrationStatus { get; init; }

    public string? RegistrationDocumentVersion { get; init; }

    /// <summary>Format YYYY-MM-DD.</summary>
    public string? RegistrationStatusEffectiveFrom { get; init; }

    /// <summary>Format YYYY-MM-DD.</summary>
    public string? RegistrationStatusEffectiveTo { get; init; }

    /// <summary>Reference of the Export Health Certificate which accompanies the animal.</summary>
    public string? EHCReference { get; init; }

    /// <summary>Original eartag of an imported animal where imported from outside the EU.</summary>
    public string? OriginalAnimalID { get; init; }

    /// <summary>Format YYYY-MM-DD.</summary>
    public string? DateOfDeath { get; init; }

    /// <summary>Format YYYY-MM-DD.</summary>
    public string? DateDeathReported { get; init; }

    public string? DeathLocation { get; init; }

    public List<AnimalGroup>? AnimalGroups { get; init; }

    public List<AnimalHealthStatus>? AnimalHealthStatus { get; init; }
}

public sealed class AnimalGroup
{
    /// <summary>e.g. FlockMark, Lot Number.</summary>
    public required string AnimalGroupNumber { get; init; }

    public required AnimalGroupMarkType AnimalGroupType { get; init; }
}

public sealed class AnimalHealthStatus
{
    /// <summary>e.g. BSE, BVD, Welfare (for herd restriction placed on by inspectorate).</summary>
    public required string AnimalHealthEventType { get; init; }

    /// <summary>e.g. Not Negative, Negative, BSE Negligible Risk, Herd Restricted (Inspectorate).</summary>
    public required string AnimalHealthEventStatus { get; init; }

    /// <summary>Format YYYY-MM-DD.</summary>
    public string? AnimalHealthEventEffectiveFrom { get; init; }

    /// <summary>Format YYYY-MM-DD.</summary>
    public string? AnimalHealthEventEffectiveTo { get; init; }
}

public sealed class HoldingDetails
{
    /// <summary>CPH of holding.</summary>
    public required string HoldingID { get; init; }

    /// <summary>Export eligibility flag.</summary>
    public string? AttestationNumber { get; init; }

    /// <summary>Format YYYY-MM-DD.</summary>
    public string? AttestationEffectiveFrom { get; init; }

    /// <summary>Format YYYY-MM-DD.</summary>
    public string? AttestationEffectiveTo { get; init; }

    /// <summary>Required by the schema, but may be an empty list.</summary>
    public required List<HoldingHealthStatus> HoldingHealthStatus { get; init; }

    public List<ExemptHoldingDetail>? ExemptHoldingDetails { get; init; }
}

public sealed class HoldingHealthStatus
{
    /// <summary>e.g. BSE, BVD, Welfare (for herd restriction placed on by inspectorate).</summary>
    public required string HoldingHealthEventType { get; init; }

    public required string HoldingHealthEventStatus { get; init; }

    /// <summary>Format YYYY-MM-DD.</summary>
    public required string HoldingHealthEventFrom { get; init; }

    /// <summary>Format YYYY-MM-DD.</summary>
    public string? HoldingHealthEventTo { get; init; }
}

public sealed class ExemptHoldingDetail
{
    /// <summary>CPH of holding included in exemption group.</summary>
    public required string ExemptHoldingID { get; init; }

    /// <summary>Format YYYY-MM-DD.</summary>
    public required string ExemptHoldingEffectiveFrom { get; init; }

    /// <summary>Format YYYY-MM-DD.</summary>
    public required string ExemptHoldingEffectiveTo { get; init; }
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

[JsonConverter(typeof(JsonStringEnumConverter<EventType>))]
public enum EventType
{
    Registration,
    Death,
    Movement,
    Import,
    Export,
    Holding,
    Animal
}

[JsonConverter(typeof(JsonStringEnumConverter<ActionType>))]
public enum ActionType
{
    New,
    Amend,
    Delete
}

[JsonConverter(typeof(JsonStringEnumConverter<Species>))]
public enum Species
{
    Sheep,
    Cattle,
    Buffalo,
    Bison,
    Goats,
    Deer,
    Pigs
}

/// <summary>01 - keeper (or The Bureau for EID Cymru); 02 - market; 03 - abattoir.</summary>
[JsonConverter(typeof(JsonStringEnumConverter<MovementType>))]
public enum MovementType
{
    [JsonStringEnumMemberName("01")] Keeper,
    [JsonStringEnumMemberName("02")] Market,
    [JsonStringEnumMemberName("03")] Abattoir
}

[JsonConverter(typeof(JsonStringEnumConverter<MovementStatus>))]
public enum MovementStatus
{
    NeedsAcceptance,
    Queried,
    Recorded,
    Completed,
    Deleted,
    Incomplete
}

[JsonConverter(typeof(JsonStringEnumConverter<SupplierType>))]
public enum SupplierType
{
    Dispatcher,
    Receiver
}

/// <summary>01=DepartureKeeper; 02=ReceivingKeeper; 03=Haulier.</summary>
[JsonConverter(typeof(JsonStringEnumConverter<AnimalTransporter>))]
public enum AnimalTransporter
{
    [JsonStringEnumMemberName("01")] DepartureKeeper,
    [JsonStringEnumMemberName("02")] ReceivingKeeper,
    [JsonStringEnumMemberName("03")] Haulier
}

/// <summary>ON, OFF, or DE (the schema does not define DE further).</summary>
[JsonConverter(typeof(JsonStringEnumConverter<MovementDirection>))]
public enum MovementDirection
{
    [JsonStringEnumMemberName("ON")] On,
    [JsonStringEnumMemberName("OFF")] Off,
    [JsonStringEnumMemberName("DE")] De
}

[JsonConverter(typeof(JsonStringEnumConverter<YesNo>))]
public enum YesNo
{
    Yes,
    No
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
