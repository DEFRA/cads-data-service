using Cads.Cds.SystemAdmin.Core.DTOs.Generation;

namespace Cads.Cds.SystemAdmin.Application.Generation.ContentGenerators;

public interface IContentGenerator
{
    IReadOnlyList<IReadOnlyDictionary<string, object?>> Generate(ICreateGenerationRequest request, int seed);
}