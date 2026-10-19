using Cads.Cds.SystemAdmin.Infrastructure.DbAdmin.Services;
using FluentAssertions;

namespace Cads.Cds.SystemAdmin.Infrastructure.Tests.Unit.DbAdmin.Services;

public class CtsImportPrepareJobQueueTests
{
    [Fact]
    public void GivenEmptyQueue_WhenTryEnqueue_ShouldReturnTrue()
    {
        var sut = new CtsImportPrepareJobQueue();

        sut.TryEnqueue("user").Should().BeTrue();
    }

    [Fact]
    public void GivenJobQueued_WhenTryEnqueueAgain_ShouldReturnFalse()
    {
        var sut = new CtsImportPrepareJobQueue();
        sut.TryEnqueue("user");

        sut.TryEnqueue("other").Should().BeFalse();
    }

    [Fact]
    public async Task GivenJobRunning_WhenTryEnqueue_ShouldReturnFalseUntilMarkedCompleted()
    {
        var sut = new CtsImportPrepareJobQueue();
        sut.TryEnqueue("user");

        await foreach (var job in sut.ReadAllAsync(TestContext.Current.CancellationToken))
        {
            job.RequestedBy.Should().Be("user");
            break;
        }

        sut.TryEnqueue("other").Should().BeFalse();

        sut.MarkCompleted();

        sut.TryEnqueue("other").Should().BeTrue();
    }
}