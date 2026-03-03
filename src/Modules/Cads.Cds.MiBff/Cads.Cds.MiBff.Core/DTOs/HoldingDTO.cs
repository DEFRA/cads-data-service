using System.Text.Json.Serialization;

namespace Cads.Cds.MiBff.Core.DTOs
{
    public class HoldingDto : IDataIdentity
    {
        [JsonPropertyName("id")]
        public required Guid Id { get; set; }

        [JsonPropertyName("name")]
        public required string Name { get; set; }

        [JsonPropertyName("cph")]
        public required string Cph { get; set; }

        [JsonPropertyName("lastModified")]
        public DateTime? LastModified { get; set; }
    }
}