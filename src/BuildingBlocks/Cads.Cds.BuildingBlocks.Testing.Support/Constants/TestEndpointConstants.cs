namespace Cads.Cds.BuildingBlocks.Testing.Support.Constants;

public static class TestEndpointConstants
{
    // BFF root url
    public const string BffMiRoot = "/api/v1/bff/mi/";

    // Amsl2 route paths
    public const string BffMiAmsl2Root = BffMiRoot + "amsl2/";

    // Amsl2 - AnimalSummary
    public const string BffAmsl2AnimalSummaryEndpoint = BffMiAmsl2Root + "animal-summary/{0}";

    // Amsl2 - DetailedMovements 
    public const string BffAmsl2DetailedMovementByIdEndpoint = BffMiAmsl2Root + "detailed-movement/{0}";

    // Amsl2 - MovementsInSuspense
    public const string BffAmsl2MovementsInSuspenseEndpoint = BffMiAmsl2Root + "movements-in-suspense";

    // Amsl2 - SummaryPremiseDetails
    public const string BffAmsl2SummaryPremiseDetailsEndpoint = BffMiAmsl2Root + "summary-premise-details";

    // Amsl2 - AnnualInventory
    public const string BffAmsl2AnnualInventoryEndpoint = BffMiAmsl2Root + "annual-inventory";

    // Amsl2 - DepartyreDetails
    public const string BffAmsl2DepartureDetailsEndpoint = BffMiAmsl2Root + "departure-details/{0}";

    // Amsl2 - DestinationDetails
    public const string BffAmsl2DestinationDetailsEndpoint = BffMiAmsl2Root + "destination-details/{0}/{1}";

    // Amsl2 - AnimalSummary
    public const string BffAmsl2AnnimalSummaryEndpoint = BffMiAmsl2Root + "animal-summary/{0}";


    // UKV route paths
    public const string BffMiUkvRoot = BffMiRoot + "ukv/";

    // UKV - Animals
    public const string BffUkvAnimalsEndpoint = BffMiUkvRoot + "animals";
    public const string BffUkvAnimalsByAnimalIdEndpoint = BffMiUkvRoot + "animals/{0}";

    // UKV - Audits/Scrapie
    public const string BffUkvAuditsScrapieEndpoint = BffMiUkvRoot + "audits/scrapie";

    // UKV - Cohorts
    public const string BffUkvCohortsEndpoint = BffMiUkvRoot + "cohorts";
    public const string BffUkvCohortsByAnimalIdEndpoint = BffMiUkvRoot + "cohorts/{0}";

    // UKV - DataQuality/Unregistered
    public const string BffUkvDataQualityUregisteredEndpoint = BffMiUkvRoot + "data-quality/unregistered";

    // UKV - Holdings
    public const string BffUkvHoldingsEndpoint = BffMiUkvRoot + "holdings";
    public const string BffUkvHoldingsByCphEndpoint = BffMiUkvRoot + "holdings/{0}";

    // UKV - Inspections/SheepGoat
    public const string BffUkvInspectionsSheepGoatEndpoint = BffMiUkvRoot + "inspections/sheep-goat";

    // UKV - Journey/Hauliers
    public const string BffUkvJourneysHauliersEndpoint = BffMiUkvRoot + "journeys/hauliers";

    // UKV - Movements
    public const string BffUkvMovementsEndpoint = BffMiUkvRoot + "movements";

    // UKV - Zones
    public const string BffUkvZonesEndpoint = BffMiUkvRoot + "zones";
    public const string BffUkvZonesByZoneIdEndpoint = BffMiUkvRoot + "zones/{0}";
}