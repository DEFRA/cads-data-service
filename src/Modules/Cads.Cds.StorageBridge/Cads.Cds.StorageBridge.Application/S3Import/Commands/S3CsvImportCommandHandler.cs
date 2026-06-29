using Cads.Cds.BuildingBlocks.Application.Commands;
using Cads.Cds.StorageBridge.Application.S3Import.Services;
using Cads.Cds.StorageBridge.Core.DTOs;

namespace Cads.Cds.StorageBridge.Application.S3Import.Commands;

public class S3CsvImportCommandHandler(IS3ImportJobEnqueuer<CreateS3CsvImportJobDto> s3ImportEnqueueService)
    : ICommandHandler<S3CsvImportCommand, Guid>
{
    public async Task<Guid> Handle(S3CsvImportCommand command, CancellationToken cancellationToken)
    {
        var job = new CreateS3CsvImportJobDto
        {
            SourceKey = command.SourceKey,
            ImportDataType = command.ImportDataType,
            ImportActionType = command.ImportActionType,
            Delimiter = command.Delimiter
        };

        return await s3ImportEnqueueService.EnqueueAsync(job, cancellationToken);
    }
}