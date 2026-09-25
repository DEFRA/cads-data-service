namespace Cads.Cds.BuildingBlocks.Infrastructure.Database.Configuration;

public static class PostgresPools
{
    // Legacy shared pools
    public const string Default = "Default";
    public const string ReadOnly = "ReadOnly";

    public const string ApiWrite = "ApiWrite";
    public const string ApiRead = "ApiRead";
    public const string MiBffWrite = "MiBffWrite";
    public const string MiBffRead = "MiBffRead";
    public const string StorageBridgeWrite = "StorageBridgeWrite";
    public const string StorageBridgeRead = "StorageBridgeRead";
    public const string SystemAdminWrite = "SystemAdminWrite";
    public const string SystemAdminRead = "SystemAdminRead";
    public const string CadsGraphQLRead = "CadsGraphQLRead";
    public const string CtsGraphQLRead = "CtsGraphQLRead";
    public const string CtsTransactionsGraphQLRead = "CtsTransactionsGraphQLRead";
    public const string CtsAuditGraphQLRead = "CtsAuditGraphQLRead";
    public const string HealthCheckWrite = "HealthCheckWrite";
    public const string HealthCheckRead = "HealthCheckRead";
}