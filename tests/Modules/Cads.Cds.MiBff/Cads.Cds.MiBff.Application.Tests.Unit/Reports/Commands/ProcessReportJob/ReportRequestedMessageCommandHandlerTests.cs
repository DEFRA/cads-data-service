using Cads.Cds.ApiSurface.Messages.Reports;
using Cads.Cds.BuildingBlocks.Application.Jobs.Services;
using Cads.Cds.BuildingBlocks.Application.Messaging.Models;
using Cads.Cds.BuildingBlocks.Application.Messaging.Serializers;
using Cads.Cds.BuildingBlocks.Core.Exceptions;
using Cads.Cds.MiBff.Application.Reports.Commands.ProcessReportJob;
using Cads.Cds.MiBff.Application.Reports.Jobs;
using FluentAssertions;
using Moq;

namespace Cads.Cds.MiBff.Application.Tests.Unit.Reports.Commands.ProcessReportJob;

public class ReportRequestedMessageCommandHandlerTests
{
    private readonly Mock<IUnwrappedMessageSerializer<ReportJobRequestedMessage>> _serializerMock = new();
    private readonly Mock<IJobEnqueuer<ProcessReportJobDto>> _jobEnqueuerMock = new();

    private ReportRequestedMessageCommandHandler CreateSut() => new(_serializerMock.Object, _jobEnqueuerMock.Object);

    private static ProcessReportJobRequestedMessageCommand NewCommand() => new(new UnwrappedMessage
    {
        MessageId = "msg-1",
        CorrelationId = "corr-1",
        Subject = "ReportJobRequested",
        Payload = "{}"
    });

    [Fact]
    public async Task Handle_WithValidMessage_EnqueuesJobAndReturnsMessage()
    {
        var jobId = Guid.NewGuid();
        var message = new ReportJobRequestedMessage { Id = Guid.NewGuid(), JobId = jobId, ReportKey = "gb_cattle_deaths" };
        _serializerMock.Setup(s => s.Deserialize(It.IsAny<UnwrappedMessage>())).Returns(message);

        var result = await CreateSut().Handle(NewCommand(), TestContext.Current.CancellationToken);

        result.Should().BeSameAs(message);
        _jobEnqueuerMock.Verify(
            e => e.EnqueueAsync(
                It.Is<ProcessReportJobDto>(j => j.ReportJobId == jobId),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Handle_WhenMessageCannotBeDeserialised_ThrowsNonRetryableWithoutEnqueuing()
    {
        _serializerMock.Setup(s => s.Deserialize(It.IsAny<UnwrappedMessage>())).Returns((ReportJobRequestedMessage?)null);

        var act = () => CreateSut().Handle(NewCommand(), TestContext.Current.CancellationToken);

        await act.Should().ThrowAsync<NonRetryableException>().WithMessage("*Deserialisation failed*msg-1*corr-1*");
        _jobEnqueuerMock.Verify(
            e => e.EnqueueAsync(It.IsAny<ProcessReportJobDto>(), It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task Handle_WhenEnqueueThrows_PropagatesException()
    {
        _serializerMock.Setup(s => s.Deserialize(It.IsAny<UnwrappedMessage>()))
            .Returns(new ReportJobRequestedMessage { JobId = Guid.NewGuid() });
        _jobEnqueuerMock.Setup(e => e.EnqueueAsync(It.IsAny<ProcessReportJobDto>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new RetryableException("later"));

        var act = () => CreateSut().Handle(NewCommand(), TestContext.Current.CancellationToken);

        await act.Should().ThrowAsync<RetryableException>();
    }
}