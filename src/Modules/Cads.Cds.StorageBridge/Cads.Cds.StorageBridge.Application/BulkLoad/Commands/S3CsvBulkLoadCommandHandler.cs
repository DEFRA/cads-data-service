using Cads.Cds.BuildingBlocks.Application.Commands;
using Cads.Cds.StorageBridge.Application.BulkLoad.Services;
using Cads.Cds.StorageBridge.Core.DTOs;

namespace Cads.Cds.StorageBridge.Application.BulkLoad.Commands;

public class S3CsvBulkLoadCommandHandler(IS3BulkLoadJobEnqueuer<CreateS3CsvBulkLoadJobDto> bulkImportEnqueueService)
    : ICommandHandler<S3CsvBulkLoadCommand, Guid>
{
    public async Task<Guid> Handle(S3CsvBulkLoadCommand command, CancellationToken cancellationToken)
    {
        var job = new CreateS3CsvBulkLoadJobDto
        {
            SourceKey = command.SourceKey,
            BulkImportType = command.BulkImportType,
            Delimiter = command.Delimiter,
            ImportActionType = command.ActionType
        };

        return await bulkImportEnqueueService.EnqueueAsync(job, cancellationToken);
    }
}