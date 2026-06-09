using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Postgres;
using FluentAssertions;

namespace Cads.Cds.StorageBridge.Testing.Support.BulkLoad.Utilities;

public delegate T RecordMapper<T>(Dictionary<string, object?> row);

public delegate T TextParser<T>(string value, char delimiter = '|');

public static class BulkLoadTestHelpers
{
    public static async Task AssertCsvRowsMatchDatabaseAsync(
        string connectionString,
        string sql,
        IEnumerable<string> csvRows)
    {
        var expected = csvRows
            .Select(row => LocationRecordUtilities.ParseLocationCsvRow(row))
            .ToList();

        var actualRows = await PostgresWaitUtilities.WaitForRowsAsync(
            connectionString,
            sql,
            expectedCount: expected.Count,
            timeout: TimeSpan.FromSeconds(10),
            pollInterval: TimeSpan.FromMilliseconds(200));

        var actual = actualRows
            .Select(LocationRecordUtilities.MapLocation)
            .ToList();

        actual.Should().BeEquivalentTo(expected);
    }

    public static async Task AssertRowsMatchDatabaseAsync<T>(
        string connectionString,
        string query,
        IEnumerable<T> dataRows,
        RecordMapper<T> recordMapper)
        where T : class
    {
        var expected = dataRows.ToList();

        var actualRows = await PostgresWaitUtilities.WaitForRowsAsync(
            connectionString,
            query,
            expectedCount: expected.Count,
            timeout: TimeSpan.FromSeconds(10),
            pollInterval: TimeSpan.FromMilliseconds(200));

        var actual = actualRows
            .Select(row => recordMapper(row))
            .ToList();

        actual.Should().BeEquivalentTo(expected);
    }
}