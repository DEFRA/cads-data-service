using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cads.Cds.BuildingBlocks.Testing.Support.Persistence;

/// <summary>
/// Fake query provider for setting up view and stored procedure data that has no keys
/// Data context must be static, therefore is shared between all tests in the same assembly.
/// </summary>
public static class FakeQueryProvider
{
    static Dictionary<Type, IQueryable> queries = new Dictionary<Type, IQueryable>();

    public static void SetQuery<T>(IEnumerable<T> query)
    {
        SetQuery(query.AsQueryable());
    }

    public static void SetQuery<T>(IQueryable<T> query)
    {
        lock (queries)
            queries[typeof(T)] = new TestAsyncEnumerable<T>(query);
    }

    public static IQueryable<T> GetQuery<T>()
    {
        lock (queries)
            if (queries.TryGetValue(typeof(T), out var query))
            {
                return (IQueryable<T>)query;
            }
            else
            {
                return Enumerable.Empty<T>().AsQueryable();
            }
    }

    public static EntityTypeBuilder<T> ToFakeQuery<T>(this EntityTypeBuilder<T> builder)
        where T : class
    {
        return builder.ToInMemoryQuery(() => GetQuery<T>());
    }
}