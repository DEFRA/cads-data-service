using Cads.Cds.ApiSurface.Messages.Reports;
using Cads.Cds.BuildingBlocks.Application.Messaging.Models;
using Cads.Cds.BuildingBlocks.Application.Messaging.Serializers;
using Cads.Cds.BuildingBlocks.Core.Exceptions;
using Cads.Cds.MiBff.Application.Reports.Commands.ProcessReportJob;
using Cads.Cds.MiBff.Application.Reports.Jobs;
using FluentAssertions;
using Moq;

namespace Cads.Cds.MiBff.Application.Tests.Unit.Reports.Commands.ProcessReportJob;

public class ProcessReportJobRequestedMessageCommandHandlerTests
{
    private readonly Mock<IUnwrappedMessageSerializer<ReportJobRequestedMessage>> _serializerMock = new();
    private readonly Mock<IReportJobProcessor> _processorMock = new();

    private ProcessReportJobRequestedMessageCommandHandler CreateSut() => new(_serializerMock.Object, _processorMock.Object);

    private static ProcessReportJobRequestedMessageCommand NewCommand() => new(new UnwrappedMessage
    {
        MessageId = "msg-1",
        CorrelationId = "corr-1",
        Subject = "ReportJobRequested",
        Payload = "{}"
    });

    [Fact]
    public async Task Handle_WithValidMessage_ProcessesJobAndReturnsMessage()
    {
        var jobId = Guid.NewGuid();
        var message = new ReportJobRequestedMessage { Id = Guid.NewGuid(), JobId = jobId, ReportKey = "gb_cattle_deaths" };
        _serializerMock.Setup(s => s.Deserialize(It.IsAny<UnwrappedMessage>())).Returns(message);

        var result = await CreateSut().Handle(NewCommand(), TestContext.Current.CancellationToken);

        result.Should().BeSameAs(message);
        _processorMock.Verify(p => p.ProcessAsync(jobId, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_WhenMessageCannotBeDeserialised_ThrowsNonRetryableWithoutProcessing()
    {
        _serializerMock.Setup(s => s.Deserialize(It.IsAny<UnwrappedMessage>())).Returns((ReportJobRequestedMessage?)null);

        var act = () => CreateSut().Handle(NewCommand(), TestContext.Current.CancellationToken);

        await act.Should().ThrowAsync<NonRetryableException>().WithMessage("*Deserialisation failed*msg-1*corr-1*");
        _processorMock.Verify(p => p.ProcessAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handle_WhenProcessorThrows_PropagatesException()
    {
        _serializerMock.Setup(s => s.Deserialize(It.IsAny<UnwrappedMessage>()))
            .Returns(new ReportJobRequestedMessage { JobId = Guid.NewGuid() });
        _processorMock.Setup(p => p.ProcessAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new RetryableException("later"));

        var act = () => CreateSut().Handle(NewCommand(), TestContext.Current.CancellationToken);

        await act.Should().ThrowAsync<RetryableException>();
    }
}
