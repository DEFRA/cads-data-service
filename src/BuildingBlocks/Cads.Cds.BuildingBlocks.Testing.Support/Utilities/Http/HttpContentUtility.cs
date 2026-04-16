using Cads.Cds.BuildingBlocks.Infrastructure.Json;
using System.Text;
using System.Text.Json;

namespace Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Http;

public static class HttpContentUtility
{
    public static StringContent CreateResponseContent<T>(T data)
    {
        var stringContent = new StringContent(
            content: JsonSerializer.Serialize(data, JsonDefaults.DefaultOptionsWithStringEnumConversion),
            encoding: Encoding.UTF8,
            mediaType: "application/json");

        return stringContent;
    }

    public static StringContent CreateResponseContent(string data)
    {
        var stringContent = new StringContent(
            content: data,
            encoding: Encoding.UTF8,
            mediaType: "application/json");

        return stringContent;
    }
}
