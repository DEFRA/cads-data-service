namespace Cads.Cds.BuildingBlocks.Testing.Support.Constants;

public static class TestEndpointConstants
{
    // UKV

    // UKV - Animals
    public const string BffUkvAnimalsEndpoint = "/api/v1/bff/ukv/animals";
    public const string BffUkvAnimalsByAnimalIdEndpoint = "/api/v1/bff/ukv/animals/{0}";

    // UKV - Audits
    public const string BffUkvAuditsScrapieEndpoint = "/api/v1/bff/ukv/audits/scrapie";

    // UKV - Cohorts
    public const string BffUkvCohortsEndpoint = "/api/v1/bff/ukv/cohorts";
    public const string BffUkvCohortsByAnimalIdEndpoint = "/api/v1/bff/ukv/cohorts/{0}";

    // UKV - DataQuality
    public const string BffUkvDataQualityUregisteredEndpoint = "/api/v1/bff/ukv/data-quality/unregistered";
   
    // UKV - Holdings
    public const string BffUkvHoldingsEndpoint = "/api/v1/bff/ukv/holdings";
    public const string BffUkvHoldingsByCphEndpoint = "/api/v1/bff/ukv/holdings/{0}";

    // UKV - Inspections
    public const string BffUkvInspectionsSheepGoatEndpoint = "/api/v1/bff/ukv/inspections/sheep-goat";

    // UKV - JourneyHauliers
    public const string BffUkvJourneysHauliersEndpoint = "/api/v1/bff/ukv/journeys/hauliers";
    
    // UKV - Movements
    public const string BffUkvMovementsEndpoint = "/api/v1/bff/ukv/movements";

    // UKV - Zones
    public const string BffUkvZonesEndpoint = "/api/v1/bff/ukv/zones";
    public const string BffUkvZonesByZoneIdEndpoint = "/api/v1/bff/ukv/zones/{0}";
}