using System.Text.Json;
using System.Text.Json.Serialization;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Json;

public static class JsonDefaults
{
    public static JsonSerializerOptions DefaultOptions { get; set; } = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = false,
        Converters = { new JsonStringEnumConverter() }
    };

    public static JsonSerializerOptions DefaultOptionsWithIndented { get; set; } = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true,
        Converters = { new JsonStringEnumConverter() }
    };
}