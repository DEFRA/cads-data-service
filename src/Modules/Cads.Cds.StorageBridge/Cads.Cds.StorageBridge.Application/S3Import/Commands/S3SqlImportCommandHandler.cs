using Cads.Cds.BuildingBlocks.Application.Commands;
using Cads.Cds.StorageBridge.Application.S3Import.Services;
using Cads.Cds.StorageBridge.Core.DTOs;

namespace Cads.Cds.StorageBridge.Application.S3Import.Commands;

public class S3SqlImportCommandHandler(IS3ImportJobEnqueuer<CreateS3SqlImportJobDto> s3ImportEnqueueService)
    : ICommandHandler<S3SqlImportCommand, Guid>
{
    public async Task<Guid> Handle(S3SqlImportCommand command, CancellationToken cancellationToken)
    {
        var job = new CreateS3SqlImportJobDto
        {
            SourceKey = command.SourceKey,
        };

        return await s3ImportEnqueueService.EnqueueAsync(job, cancellationToken);
    }
}