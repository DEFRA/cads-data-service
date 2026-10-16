namespace Cads.Cds.SystemAdmin.Application.Generation.ContentGenerators;

public interface IContentGenerator
{
    string TableName { get; }
    IReadOnlyList<IReadOnlyDictionary<string, object?>> Generate(IEnumerable<decimal> businessKeys, int seed);
}