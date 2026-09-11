using Cads.Cds.BuildingBlocks.Application.Commands;
using Cads.Cds.SystemAdmin.Core.DTOs.Generation;

namespace Cads.Cds.SystemAdmin.Application.Generation.Commands.CreateGeneration;

public class CreateGenerationCommandHandler : ICommandHandler<CreateGenerationCommand, CreateGenerationResponseDto>
{
    public async Task<CreateGenerationResponseDto> Handle(CreateGenerationCommand request, CancellationToken cancellationToken)
    {
        return new CreateGenerationResponseDto 
        { 
            Content = $"Generated content for table {request.Table} with scenario {request.Scenario} and row count {request.RowCount}.",
            FileName = $"{request.Table}_{request.Scenario}_{DateTime.UtcNow:yyyyMMddHHmmss}.txt",
            Keys = Enumerable.Range((int)request.Key, request.RowCount).Select(i => (long)i)
        };
    }
}
