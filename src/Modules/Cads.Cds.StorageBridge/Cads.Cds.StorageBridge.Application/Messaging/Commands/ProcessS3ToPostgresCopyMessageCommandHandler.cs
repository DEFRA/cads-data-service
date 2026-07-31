using Cads.Cds.BuildingBlocks.Application.Commands;
using Cads.Cds.BuildingBlocks.Application.Messaging.Messages;
using Cads.Cds.BuildingBlocks.Application.Messaging.Serializers;
using Cads.Cds.BuildingBlocks.Core.Exceptions;
using Cads.Cds.StorageBridge.Application.Messaging.Messages;

namespace Cads.Cds.StorageBridge.Application.Messaging.Commands;

public class ProcessS3ToPostgresCopyMessageCommandHandler(
    IUnwrappedMessageSerializer<S3ToPostgresCopyMessage> serializer)
    : ICommandHandler<ProcessS3ToPostgresCopyMessageCommand, MessageType>
{
    private readonly IUnwrappedMessageSerializer<S3ToPostgresCopyMessage> _serializer = serializer;

    public async Task<MessageType> Handle(ProcessS3ToPostgresCopyMessageCommand request, CancellationToken cancellationToken)
    {
        var message = request.Message;

        ArgumentNullException.ThrowIfNull(message);

        var messagePayload = _serializer.Deserialize(message)
            ?? throw new NonRetryableException($"Deserialisation failed or the message payload was null for " +
            $"messageType: {typeof(S3ToPostgresCopyMessage).Name}," +
            $"messageId: {message.MessageId}," +
            $"correlationId: {message.CorrelationId}");

        // TODO - Add implementation

        return await Task.FromResult(messagePayload);
    }
}