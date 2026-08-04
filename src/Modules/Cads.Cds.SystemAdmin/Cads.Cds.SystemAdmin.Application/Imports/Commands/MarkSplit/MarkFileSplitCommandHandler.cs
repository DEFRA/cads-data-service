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

namespace Cads.Cds.SystemAdmin.Application.Imports.Commands.MarkSplit;

public sealed class MarkFileSplitCommandHandler(
    ISystemAdminFileImportRepository fileImportRepository,
    IMessagePublisher<SystemAdminFifoQueueClient> messagePublisher,
    IOptions<ImportsDeduplication> importsDeduplicationOptions,
    ILogger<MarkFileSplitCommandHandler> logger)
    : ICommandHandler<MarkFileSplitCommand, Unit>
{
    private readonly string _bucketName = importsDeduplicationOptions?.Value.BucketName ?? Guid.NewGuid().ToString();
    private readonly string _oracleEnvironment = importsDeduplicationOptions?.Value.EnvironmentName ?? Guid.NewGuid().ToString();

    public async Task<Unit> Handle(MarkFileSplitCommand command, CancellationToken cancellationToken)
    {
        var fileImport = await fileImportRepository.GetById(command.Id, cancellationToken)
                         ?? throw new NotFoundException(nameof(FileImport), command.Id);

        fileImport.MarkSplit();

        await PublishS3ToPostgresCopyMessage(fileImport.Id, fileImport.FileName, cancellationToken);

        return Unit.Value;
    }

    private async Task PublishS3ToPostgresCopyMessage(long fileId, string objectKey, CancellationToken cancellationToken)
    {
        var correlationId = CorrelationIdContext.Value ?? Guid.NewGuid().ToString();
        var messageGroupId = DeduplicationKeyGenerator.GenerateMessageGroupId(objectKey, _oracleEnvironment);
        var messageDeduplicationId = DeduplicationKeyGenerator.GenerateDeduplicationId(_bucketName, objectKey, fileId.ToString(), _oracleEnvironment);

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
                FileImportId = fileId,
                ObjectKey = objectKey
            };

            var metadata = new FifoMessageMetadata(
                messageGroupId,
                messageDeduplicationId,
                correlationId);

            if (logger.IsEnabled(LogLevel.Information))
            {
                logger.LogInformation("MarkFileSplitCommandHandler publishing S3ToPostgresCopyMessage");
            }

            await messagePublisher.PublishAsync(message, metadata, cancellationToken);
        }
    }
}