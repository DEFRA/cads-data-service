using Cads.Cds.MiBff.Application.Reports.Commands.CreateReportJob;
using Cads.Cds.MiBff.Application.Reports.Requests;
using Cads.Cds.MiBff.Application.Reports.Routing;
using Cads.Cds.MiBff.Application.Reports.Routing.Abstractions;
using Cads.Cds.MiBff.Core.Domain.Entities;
using Cads.Cds.MiBff.Core.Domain.Repositories;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.MiBff.Application.Tests.Unit.Reports.Commands.CreateReportJob;

public class CreateReportJobCommandHandlerTests
{
    private readonly FakeMiReportJobRepository _repository = new();

    public CreateReportJobCommandHandlerTests()
    {
        var services = new ServiceCollection();
        services.AddTransient<TestReportHandler>();
    }

    private CreateReportJobCommandHandler CreateHandler()
        => new(_repository);

    [Fact]
    public async Task Handle_WithValidReportKey_PersistsJobAndOutboxAndReturnsJobId()
    {
        var handler = CreateHandler();

        var command = new CreateReportJobCommand
        {
            ReportKey = "TestCreateReportJob",
            ExternalSubject = "user@example.test",
            Request = new TestCreateReportJobRequest { Year = 2026 }
        };

        var result = await handler.Handle(command, TestContext.Current.CancellationToken);

        result.Should().NotBe(Guid.Empty);

        _repository.CreatedJob.Should().NotBeNull();
        _repository.CreatedJob!.ReportJobId.Should().Be(result);
        _repository.CreatedJob.ReportKey.Should().Be("TestCreateReportJob");
        _repository.CreatedJob.ExternalSubject.Should().Be("user@example.test");
        _repository.CreatedJob.Status.Should().Be(ReportJobStatus.Pending);
        _repository.CreatedJob.RequestPayload.Should().Contain("\"year\":2026");

        _repository.CreatedOutbox.Should().NotBeNull();
        _repository.CreatedOutbox!.ReportJobId.Should().Be(result);
        _repository.CreatedOutbox.Status.Should().Be(ReportJobOutboxStatus.Pending);
        _repository.CreatedOutbox.Subject.Should().Be(ReportJobOutboxSubjects.ReportJobRequested);
        _repository.CreatedOutbox.Payload.Should().Contain(result.ToString());
    }

    [Fact]
    public async Task Handle_WithUnknownReportKey_ThrowsKeyNotFoundException()
    {
        var handler = CreateHandler();

        var command = new CreateReportJobCommand
        {
            ReportKey = "DoesNotExist",
            ExternalSubject = "user@example.test",
            Request = new TestCreateReportJobRequest()
        };

        var act = () => handler.Handle(command, TestContext.Current.CancellationToken);

        await act.Should().ThrowAsync<KeyNotFoundException>();

        _repository.CreatedJob.Should().BeNull();
        _repository.CreatedOutbox.Should().BeNull();
    }

    [ReportHandler("TestCreateReportJob", typeof(TestCreateReportJobRequest))]
    public class TestReportHandler : IReportHandler
    {
        public object BuildUntypedQuery(GetReportRequest request) => throw new NotImplementedException();
    }

    public class TestCreateReportJobRequest : GetReportRequest
    {
        public int Year { get; set; }
    }

    private sealed class FakeMiReportJobRepository : IMiReportJobRepository
    {
        public MiReportJob? CreatedJob { get; private set; }
        public MiReportJobOutbox? CreatedOutbox { get; private set; }

        public Task CreateJobWithOutboxAsync(MiReportJob reportJob, MiReportJobOutbox outboxMessage, CancellationToken cancellationToken = default)
        {
            CreatedJob = reportJob;
            CreatedOutbox = outboxMessage;
            return Task.CompletedTask;
        }

        public Task<MiReportJob?> GetByReportJobIdAsync(Guid reportJobId, CancellationToken cancellationToken = default)
            => throw new NotImplementedException();

        public Task UpdateStatusAsync(
            Guid reportJobId,
            string status,
            string? s3Key = null,
            int? rowCount = null,
            string? error = null,
            DateTimeOffset? startedAt = null,
            DateTimeOffset? completedAt = null,
            DateTimeOffset? expiresAt = null,
            CancellationToken cancellationToken = default)
            => throw new NotImplementedException();

        public Task<IReadOnlyList<MiReportJobOutbox>> GetPendingOutboxMessagesAsync(int batchSize = 50, CancellationToken cancellationToken = default)
            => throw new NotImplementedException();

        public Task DeleteDispatchedOutboxMessagesAsync(IEnumerable<Guid> outboxIds, CancellationToken cancellationToken = default)
            => throw new NotImplementedException();
    }
}