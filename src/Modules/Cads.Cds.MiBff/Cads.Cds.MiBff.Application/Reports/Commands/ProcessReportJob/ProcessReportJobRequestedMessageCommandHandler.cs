using Cads.Cds.ApiSurface.Messages;
using Cads.Cds.ApiSurface.Messages.Reports;
using Cads.Cds.BuildingBlocks.Application.Commands;
using Cads.Cds.BuildingBlocks.Application.Messaging.Serializers;
using Cads.Cds.BuildingBlocks.Core.Exceptions;
using Cads.Cds.MiBff.Application.Reports.Jobs;

namespace Cads.Cds.MiBff.Application.Reports.Commands.ProcessReportJob;

/// <summary>
/// Consumes a <see cref="ReportJobRequestedMessage"/> from the MiBff FIFO queue and hands the job
/// to <see cref="IReportJobProcessor"/>.
/// </summary>
public class ProcessReportJobRequestedMessageCommandHandler(
    IUnwrappedMessageSerializer<ReportJobRequestedMessage> serializer,
    IReportJobProcessor reportJobProcessor)
    : ICommandHandler<ProcessReportJobRequestedMessageCommand, MessageType>
{
    public async Task<MessageType> Handle(ProcessReportJobRequestedMessageCommand request, CancellationToken cancellationToken)
    {
        var message = request.Message;

        ArgumentNullException.ThrowIfNull(message);

        var messagePayload = serializer.Deserialize(message)
            ?? throw new NonRetryableException($"Deserialisation failed or the message payload was null for " +
                $"messageType: {nameof(ReportJobRequestedMessage)}, " +
                $"messageId: {message.MessageId}, " +
                $"correlationId: {message.CorrelationId}");

        await reportJobProcessor.ProcessAsync(messagePayload.JobId, cancellationToken);

        return messagePayload;
    }
}
