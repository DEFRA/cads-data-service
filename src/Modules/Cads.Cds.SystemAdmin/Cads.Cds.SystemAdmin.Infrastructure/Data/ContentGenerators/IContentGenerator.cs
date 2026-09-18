using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.Data.ContentGenerators;

public interface IContentGenerator
{
    string Table { get; }
    IReadOnlyList<IReadOnlyDictionary<string, object?>> GenerateBulk(IReadOnlyList<decimal> businessKeys, int seed);
    IReadOnlyDictionary<string, object?> GenerateUpdate(decimal businessKey, int seed);
}
