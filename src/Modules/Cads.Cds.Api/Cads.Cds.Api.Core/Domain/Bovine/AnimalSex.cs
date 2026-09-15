using System.Text.Json.Serialization;

namespace Cads.Cds.Api.Core.Domain.Bovine;

[JsonConverter(typeof(JsonStringEnumConverter<AnimalSex>))]
public enum AnimalSex
{
    Female,
    Male
}
