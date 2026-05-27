using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.CircuitBreakers;

namespace Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Postgres;

public static class PostgresWaitUtilities
{
    public static Task<List<Dictionary<string, object?>>> WaitForRowsAsync(
        string connectionString,
        string tableName,
        string orderBy,
        int expectedCount,
        TimeSpan timeout,
        TimeSpan pollInterval)
    {
        return CircuitBreakerUtilities.WaitUntilAsync(
            action: () => PostgresQueryUtilities.QueryRowsAsync(connectionString, tableName, orderBy),
            condition: rows => rows?.Count == expectedCount,
            timeout: timeout,
            pollInterval: pollInterval);
    }
}