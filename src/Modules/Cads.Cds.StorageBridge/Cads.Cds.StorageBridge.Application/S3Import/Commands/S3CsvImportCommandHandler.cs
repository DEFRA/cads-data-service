using Cads.Cds.BuildingBlocks.Application.Commands;
using Cads.Cds.BuildingBlocks.Core.Domain.Imports;
using Cads.Cds.StorageBridge.Application.Imports.Repositories;
using Cads.Cds.StorageBridge.Application.S3Import.Services;
using Cads.Cds.StorageBridge.Core.DTOs;

namespace Cads.Cds.StorageBridge.Application.S3Import.Commands;

public class S3CsvImportCommandHandler(IS3ImportJobEnqueuer<CreateS3CsvImportJobDto> s3ImportEnqueueService, IStorageBridgeFileImportRepository fileImportRepository)
    : ICommandHandler<S3CsvImportCommand, Guid>
{
    public async Task<Guid> Handle(S3CsvImportCommand command, CancellationToken cancellationToken)
    {
        var fileImportId = command.FileImportId;

        if (fileImportId is null)
        {
            FileImport? fileImport;

            if (string.IsNullOrEmpty(command.SourceKey))
            {
                throw new ArgumentException("FileImport SourceKey is required.", nameof(command));
            }

            fileImport = await fileImportRepository.GetByFileNameAsync(command.SourceKey, cancellationToken);

            if (fileImport == null)
            {
                throw new InvalidOperationException($"FileImport with SourceKey {command.SourceKey} not found.");
            }

            fileImportId = fileImport.Id;
        }

        var job = new CreateS3CsvImportJobDto
        {
            FileImportId = fileImportId.GetValueOrDefault(),
            Delimiter = command.Delimiter
        };

        return await s3ImportEnqueueService.EnqueueAsync(job, cancellationToken);
    }
}