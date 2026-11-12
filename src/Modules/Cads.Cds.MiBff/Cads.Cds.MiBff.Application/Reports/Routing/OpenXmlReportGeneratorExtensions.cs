using Cads.Cds.BuildingBlocks.Core.OpenXml;
using System.Reflection;
using System.Text.Json;

namespace Cads.Cds.MiBff.Application.Reports.Routing;

public static class OpenXmlReportGeneratorExtensions
{
    private static readonly MethodInfo GenerateMethod =
        typeof(IOpenXmlReportGenerator).GetMethod(nameof(IOpenXmlReportGenerator.Generate))!;

    public static async Task<MemoryStream> GenerateFromJsonAsync(
        this IOpenXmlReportGenerator generator,
        Stream jsonData,
        Type modelType,
        JsonSerializerOptions? jsonOptions = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(generator);
        ArgumentNullException.ThrowIfNull(jsonData);
        ArgumentNullException.ThrowIfNull(modelType);

        var listType = typeof(List<>).MakeGenericType(modelType);

        var data = await JsonSerializer.DeserializeAsync(
            jsonData, listType, jsonOptions ?? JsonSerializerOptions.Web, cancellationToken)
            ?? Activator.CreateInstance(listType)!;

        var generic = GenerateMethod.MakeGenericMethod(modelType);

        return (MemoryStream)generic.Invoke(generator, [data])!;
    }
}

