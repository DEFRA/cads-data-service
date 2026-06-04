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
        var sql = $"SELECT * FROM {tableName} ORDER BY {orderBy}";

        return WaitForRowsAsync(
            connectionString,
            sql,
            expectedCount,
            timeout,
            pollInterval);
    }

    public static Task<List<Dictionary<string, object?>>> WaitForRowsAsync(
        string connectionString,
        string sql,
        int expectedCount,
        TimeSpan timeout,
        TimeSpan pollInterval)
    {
        return CircuitBreakerUtilities.WaitUntilAsync(
            action: () => PostgresQueryUtilities.QueryAsync(connectionString, sql),
            condition: rows => rows?.Count == expectedCount,
            timeout: timeout,
            pollInterval: pollInterval);
    }
}