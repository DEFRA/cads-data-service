using Cads.Cds.BuildingBlocks.Application.Commands;
using Cads.Cds.StorageBridge.Core.DTOs;
using Cads.Cds.StorageBridge.Core.Services;

namespace Cads.Cds.StorageBridge.Application.Commands.BulkImport;

public class BulkImportCommandHandler(IBulkImportEnqueueService bulkImportEnqueueService) : ICommandHandler<BulkImportCommand, Guid>
{
    public async Task<Guid> Handle(BulkImportCommand command, CancellationToken cancellationToken = default)
    {
        var dto = new CreateBulkImportJobDto
        {
            SourceKey = command.SourceKey,
            BulkImportType = command.BulkImportType,
            Delimiter = command.Delimiter,
            ImportActionType = command.ImportActionType
        };

        return await bulkImportEnqueueService.EnqueueAsync(dto, cancellationToken);
    }
}