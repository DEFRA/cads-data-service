using Cads.Cds.Api.Application.DTOs.Bovine.Animals;
using Cads.Cds.BuildingBlocks.Application.Queries;

namespace Cads.Cds.Api.Application.Queries.Bovine.AnimalDetails;

public class GetAnimalDetailsByIdentifier : IQuery<AnimalDetailsDto?>
{
    public required string Identifier { get; set; }
}