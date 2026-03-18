using Cads.Cds.MiBff.Core.Domain.DTOs;
using System.Text.Json.Serialization;

namespace Cads.Cds.MiBff.Core.DTOs.Common;

public abstract class SampleDto : IDataIdentity
{
    [JsonPropertyName("id")]
    public required Guid Id { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("code")]
    public required string Code { get; set; }

    [JsonPropertyName("lastModified")]
    public DateTime? LastModified { get; set; }
}