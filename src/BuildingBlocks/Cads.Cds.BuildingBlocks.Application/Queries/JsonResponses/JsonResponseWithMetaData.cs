using System.Text.Json.Serialization;

namespace Cads.Cds.BuildingBlocks.Application.Queries.JsonResponses;

public class JsonResponseWithMetaData
{
    [JsonPropertyName("meta")]
    public JsonResponseMetaData? Meta { get; set; }

    [JsonPropertyName("data")]
    public object? Data { get; set; }

    [JsonPropertyName("links")]
    public JsonResponseLinks? Links { get; set; }
}

public class JsonResponseMetaData
{
    [JsonPropertyName("service")]
    public string? Service { get; set; }

    [JsonPropertyName("version")]
    public string? Version { get; set; }

    [JsonPropertyName("requestId")]
    public string? RequestId { get; set; }

    [JsonPropertyName("timestamp")]
    public DateTime? Timestamp { get; set; }

    [JsonPropertyName("endpoint")]
    public string? Endpoint { get; set; }

    [JsonPropertyName("status")]
    public string? Status { get; set; }
}

public class JsonResponseData<T>
{
    [JsonPropertyName("message")]
    public string? Message { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("parameters")]
    public JsonResponseDataParameters? Parameters { get; set; }

    [JsonPropertyName("results")]
    public IEnumerable<T> Results { get; set; } = [];
}

public class JsonResponseLinks
{
    [JsonPropertyName("self")]
    public string? Self { get; set; }
}

public class JsonResponseDataParameters
{
    [JsonPropertyName("path")]
    public string? Path { get; set; }

    [JsonPropertyName("query")]
    public string? Query { get; set; }
}