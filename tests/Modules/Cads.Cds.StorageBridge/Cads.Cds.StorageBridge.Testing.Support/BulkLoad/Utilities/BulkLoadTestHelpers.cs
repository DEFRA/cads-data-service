using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Postgres;
using FluentAssertions;

namespace Cads.Cds.StorageBridge.Testing.Support.BulkLoad.Utilities;

public static class BulkLoadTestHelpers
{
    public static async Task AssertCsvRowsMatchDatabaseAsync(
        string connectionString,
        string tableName,
        IEnumerable<string> csvRows,
        string orderBy)
    {
        var expected = csvRows
            .Select(LocationRecordUtilities.ParseLocationCsvRow)
            .ToList();

        var actualRows = await PostgresWaitUtilities.WaitForRowsAsync(
            connectionString,
            tableName,
            orderBy,
            expectedCount: expected.Count,
            timeout: TimeSpan.FromSeconds(10),
            pollInterval: TimeSpan.FromMilliseconds(200));

        var actual = actualRows
            .Select(LocationRecordUtilities.MapLocationFromDb)
            .ToList();

        actual.Should().BeEquivalentTo(expected);
    }
}