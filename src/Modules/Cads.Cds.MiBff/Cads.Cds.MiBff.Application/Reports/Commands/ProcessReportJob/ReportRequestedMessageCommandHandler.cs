using Cads.Cds.ApiSurface.Messages;
using Cads.Cds.ApiSurface.Messages.Reports;
using Cads.Cds.BuildingBlocks.Application.Commands;
using Cads.Cds.BuildingBlocks.Application.Jobs.Services;
using Cads.Cds.BuildingBlocks.Application.Messaging.Serializers;
using Cads.Cds.BuildingBlocks.Core.Correlation;
using Cads.Cds.BuildingBlocks.Core.Exceptions;
using Cads.Cds.MiBff.Application.Reports.Jobs;

namespace Cads.Cds.MiBff.Application.Reports.Commands.ProcessReportJob;

public class ReportRequestedMessageCommandHandler(
    IUnwrappedMessageSerializer<ReportJobRequestedMessage> serializer,
    IJobEnqueuer<ProcessReportJobDto> jobEnqueuer)
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

        await jobEnqueuer.EnqueueAsync(
            new ProcessReportJobDto
            {
                ReportJobId = messagePayload.JobId,
                CorrelationId = CorrelationIdContext.Value
            },
            cancellationToken);

        return messagePayload;
    }
}