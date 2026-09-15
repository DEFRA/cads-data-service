using System.Text.Json.Serialization;

namespace Cads.Cds.Api.Core.Domain.Bovine;

[JsonConverter(typeof(JsonStringEnumConverter<AnimalSpecies>))]
public enum AnimalSpecies
{
    Cattle
}