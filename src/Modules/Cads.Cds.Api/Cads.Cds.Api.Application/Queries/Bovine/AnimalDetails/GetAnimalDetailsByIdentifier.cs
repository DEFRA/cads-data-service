using Cads.Cds.Api.Core.DTOs.Bovine;
using Cads.Cds.BuildingBlocks.Application.Queries;

namespace Cads.Cds.Api.Application.Queries.Bovine.AnimalDetails;

public class GetAnimalDetailsByIdentifier : IQuery<AnimalDetailDto?>
{
    public required string Identifier { get; set; }
}
