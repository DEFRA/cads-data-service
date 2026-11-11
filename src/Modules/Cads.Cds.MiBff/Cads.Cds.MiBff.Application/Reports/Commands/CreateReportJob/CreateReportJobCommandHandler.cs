using Cads.Cds.BuildingBlocks.Application.Commands;
using Cads.Cds.MiBff.Core.Domain.Constants;
using Cads.Cds.MiBff.Core.Domain.Entities;
using Cads.Cds.MiBff.Core.Domain.Repositories;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Cads.Cds.MiBff.Application.Reports.Commands.CreateReportJob;

public class CreateReportJobCommandHandler(
    IMiReportJobRepository reportJobRepository)
    : ICommandHandler<CreateReportJobCommand, Guid>
{
    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Converters = { new JsonStringEnumConverter() }
    };

    public async Task<Guid> Handle(CreateReportJobCommand command, CancellationToken cancellationToken)
    {
        var now = DateTimeOffset.UtcNow;
        var reportJobId = Guid.NewGuid();

        var reportJob = new MiReportJob
        {
            ReportJobId = reportJobId,
            ReportKey = command.ReportKey,
            ExternalSubject = command.ExternalSubject,
            RequestPayload = JsonSerializer.Serialize(command.Request, command.Request.GetType(), SerializerOptions),
            Status = ReportJobStatus.Pending,
            CreatedAt = now,
            UpdatedAt = now
        };

        var outboxMessage = new MiReportJobOutbox
        {
            OutboxId = Guid.NewGuid(),
            ReportJobId = reportJobId,
            Subject = ReportJobOutboxSubjects.ReportJobRequested,
            Payload = JsonSerializer.Serialize(new
            {
                reportJobId,
                reportKey = command.ReportKey,
                externalSubject = command.ExternalSubject,
                requestPayload = reportJob.RequestPayload
            }, SerializerOptions),
            Status = ReportJobOutboxStatus.Pending,
            Attempts = 0,
            CreatedAt = now
        };

        await reportJobRepository.CreateJobWithOutboxAsync(reportJob, outboxMessage, cancellationToken);

        return reportJobId;
    }
}