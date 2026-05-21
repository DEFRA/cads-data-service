using Npgsql;
using Xunit;

namespace Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Postgres;

public static class PostgresTestHelpers
{
    public static async Task<long> WaitForRowCountAsync(
        string connectionString,
        string sql,
        long expectedCount,
        TimeSpan timeout,
        TimeSpan pollInterval)
    {
        var start = DateTime.UtcNow;

        while (DateTime.UtcNow - start < timeout)
        {
            await using var conn = new NpgsqlConnection(connectionString);
            await conn.OpenAsync();

            await using var cmd = new NpgsqlCommand(sql, conn);
            var count = (long?)await cmd.ExecuteScalarAsync(TestContext.Current.CancellationToken) ?? 0;

            if (count == expectedCount)
                return count;

            await Task.Delay(pollInterval);
        }

        throw new TimeoutException(
            $"Timed out waiting for row count {expectedCount} for query: {sql}");
    }
}
