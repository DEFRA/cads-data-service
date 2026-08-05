using Cads.Cds.ApiSurface.Dtos.Imports;
using Cads.Cds.ApiSurface.Messages.Imports;
using Cads.Cds.BuildingBlocks.Application.Commands;
using Cads.Cds.BuildingBlocks.Application.Messaging.Models;
using Cads.Cds.BuildingBlocks.Application.Messaging.Publishers;
using Cads.Cds.BuildingBlocks.Core.Correlation;
using Cads.Cds.BuildingBlocks.Core.Domain.Imports;
using Cads.Cds.BuildingBlocks.Core.Exceptions;
using Cads.Cds.SystemAdmin.Application.Imports.Configuration;
using Cads.Cds.SystemAdmin.Application.Imports.Repositories;
using Cads.Cds.SystemAdmin.Application.Messaging.Clients;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Cads.Cds.SystemAdmin.Application.Imports.Commands.UpdateFileImport;

public class UpdateFileImportCommandHandler(
    ISystemAdminFileImportRepository fileImportRepository,
    IMessagePublisher<SystemAdminFifoQueueClient> messagePublisher,
    IOptions<ImportsDeduplication> importsDeduplicationOptions,
    ILogger<UpdateFileImportCommandHandler> logger)
    : ICommandHandler<UpdateFileImportCommand, Unit>
{
    public async Task<Unit> Handle(UpdateFileImportCommand command, CancellationToken cancellationToken)
    {
        var fileImport = await fileImportRepository.GetById(command.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(FileImport), command.Id);

        var transitionToStateSplit = fileImport.ImportStatus != FileImportStatus.Split &&
            command.ImportStatus == FileImportStatus.Split;

        fileImport.SetTotalRowsToProcess(command.TotalRowsToProcess);
        fileImport.SetRowsFound(command.RowsFound);
        fileImport.SetImportStatus(command.ImportStatus);

        if (transitionToStateSplit)
        {
            await PublishS3ToPostgresCopyMessage(fileImport.Id, fileImport.FileName, cancellationToken);
        }

        return Unit.Value;
    }

    private async Task PublishS3ToPostgresCopyMessage(long fileImportId, string objectKey, CancellationToken cancellationToken)
    {
        var bucketName = importsDeduplicationOptions?.Value.BucketName ?? Guid.NewGuid().ToString();
        var oracleEnvironment = importsDeduplicationOptions?.Value.EnvironmentName ?? Guid.NewGuid().ToString();

        var correlationId = CorrelationIdContext.Value ?? Guid.NewGuid().ToString();
        var messageGroupId = DeduplicationKeyGenerator.GenerateMessageGroupId(objectKey, oracleEnvironment);
        var messageDeduplicationId = DeduplicationKeyGenerator.GenerateDeduplicationId(
            bucketName,
            objectKey,
            fileImportId.ToString(),
            oracleEnvironment);

        using (logger.BeginScope(new Dictionary<string, object?>
        {
            ["CorrelationId"] = correlationId,
            ["GroupId"] = messageGroupId,
            ["DeduplicationId"] = messageDeduplicationId
        }))
        {
            var message = new S3ToPostgresCopyMessage
            {
                Id = DeterministicGuid.From(messageDeduplicationId),
                FileImportId = fileImportId,
                ObjectKey = objectKey
            };

            var metadata = new FifoMessageMetadata(
                messageGroupId,
                messageDeduplicationId,
                correlationId);

            if (logger.IsEnabled(LogLevel.Information))
            {
                logger.LogInformation("UpdateFileImportCommandHandler publishing S3ToPostgresCopyMessage");
            }

            await messagePublisher.PublishAsync(message, metadata, cancellationToken);
        }
    }
}