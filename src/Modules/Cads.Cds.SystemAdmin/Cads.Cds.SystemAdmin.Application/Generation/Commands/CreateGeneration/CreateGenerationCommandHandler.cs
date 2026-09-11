using Cads.Cds.BuildingBlocks.Application.Commands;
using Cads.Cds.SystemAdmin.Core.DTOs.Generation;

namespace Cads.Cds.SystemAdmin.Application.Generation.Commands.CreateGeneration;

public class CreateGenerationCommandHandler : ICommandHandler<CreateGenerationCommand, CreateGenerationResponseDto>
{
    public async Task<CreateGenerationResponseDto> Handle(CreateGenerationCommand request, CancellationToken cancellationToken)
    {
        IEnumerable<long> businessKeys = [];

        if (request.BusinessKey.HasValue)
        {
            businessKeys = Enumerable.Range((int)request.BusinessKey.Value, request.RowCount).Select(i => (long)i);
        }
        else
        {
            businessKeys = Enumerable.Range(1, request.RowCount).Select(i => (long)i);
        }

        return new CreateGenerationResponseDto
        {
            Content = $"Generated content for table {request.Table} with scenario {request.Scenario} and row count {request.RowCount}.",
            FileName = $"{request.Table}_{request.Scenario}_{DateTime.UtcNow:yyyyMMddHHmmss}.txt",
            BusinessKeys = businessKeys
        };
    }
}