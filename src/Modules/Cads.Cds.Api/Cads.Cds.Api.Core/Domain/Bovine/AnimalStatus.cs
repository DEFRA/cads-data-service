using System.Text.Json.Serialization;

namespace Cads.Cds.Api.Core.Domain.Bovine;

[JsonConverter(typeof(JsonStringEnumConverter<AnimalStatus>))]
public enum AnimalStatus
{
    Alive,
    Dead,
    OffFarm,
    Unknown
}