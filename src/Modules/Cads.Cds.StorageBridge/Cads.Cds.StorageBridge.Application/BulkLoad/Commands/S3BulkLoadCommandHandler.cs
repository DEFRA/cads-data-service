using Cads.Cds.BuildingBlocks.Application.Commands;
using Cads.Cds.StorageBridge.Application.BulkLoad.Services;
using Cads.Cds.StorageBridge.Core.DTOs;

namespace Cads.Cds.StorageBridge.Application.BulkLoad.Commands;

public class S3BulkLoadCommandHandler(IS3BulkLoadJobEnqueuer bulkImportEnqueueService)
    : ICommandHandler<S3BulkLoadCommand, Guid>
{
    public async Task<Guid> Handle(S3BulkLoadCommand command, CancellationToken cancellationToken)
    {
        var createS3BulkLoadJob = new CreateS3BulkLoadJobDto
        {
            SourceKey = command.SourceKey,
            BulkImportType = command.BulkImportType,
            Delimiter = command.Delimiter,
            ImportActionType = command.ImportActionType
        };

        return await bulkImportEnqueueService.EnqueueAsync(createS3BulkLoadJob, cancellationToken);
    }
}