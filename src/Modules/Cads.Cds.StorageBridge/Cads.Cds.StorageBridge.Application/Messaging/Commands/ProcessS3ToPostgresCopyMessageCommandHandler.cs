using Cads.Cds.ApiSurface.Messages;
using Cads.Cds.ApiSurface.Messages.Imports;
using Cads.Cds.BuildingBlocks.Application.Commands;
using Cads.Cds.BuildingBlocks.Application.Messaging.Serializers;
using Cads.Cds.BuildingBlocks.Core.Exceptions;
using Cads.Cds.StorageBridge.Application.S3Import.Services;
using Cads.Cds.StorageBridge.Core.DTOs;
using Microsoft.Extensions.Logging;

namespace Cads.Cds.StorageBridge.Application.Messaging.Commands;

public class ProcessS3ToPostgresCopyMessageCommandHandler(
    IS3ImportJobEnqueuer<CreateS3CsvImportJobDto> s3ImportEnqueueService,
    IUnwrappedMessageSerializer<S3ToPostgresCopyMessage> serializer,
    ILogger<ProcessS3ToPostgresCopyMessageCommandHandler> logger)
    : ICommandHandler<ProcessS3ToPostgresCopyMessageCommand, MessageType>
{
    private readonly IUnwrappedMessageSerializer<S3ToPostgresCopyMessage> _serializer = serializer;

    public async Task<MessageType> Handle(ProcessS3ToPostgresCopyMessageCommand request, CancellationToken cancellationToken)
    {
        var message = request.Message;

        ArgumentNullException.ThrowIfNull(message);

        using (logger.BeginScope(new Dictionary<string, object?>
        {
            ["CorrelationId"] = message.CorrelationId
        }))
        {
            var messagePayload = _serializer.Deserialize(message)
                ?? throw new NonRetryableException($"Deserialisation failed or the message payload was null for " +
                $"messageType: {typeof(S3ToPostgresCopyMessage).Name}," +
                $"messageId: {message.MessageId}," +
                $"correlationId: {message.CorrelationId}");

            var job = new CreateS3CsvImportJobDto
            {
                FileImportId = messagePayload.FileImportId,
                SourceKey = messagePayload.ObjectKey,
                CorrelationId = messagePayload.CorrelationId
            };

            if (logger.IsEnabled(LogLevel.Information))
            {
                logger.LogInformation("Enqueueing CreateS3SqlImportJob with FileImportId={FileImportId}, ObjectKey={ObjectKey}",
                    messagePayload.FileImportId, messagePayload.ObjectKey);
            }

            await s3ImportEnqueueService.EnqueueAsync(job, cancellationToken);

            return messagePayload;
        }
    }
}