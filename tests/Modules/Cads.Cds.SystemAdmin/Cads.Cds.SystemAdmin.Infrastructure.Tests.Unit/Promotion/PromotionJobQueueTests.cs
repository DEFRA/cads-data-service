using Cads.Cds.SystemAdmin.Core.Promotion;
using Cads.Cds.SystemAdmin.Infrastructure.Promotion.Channels;
using FluentAssertions;

namespace Cads.Cds.SystemAdmin.Infrastructure.Tests.Unit.Promotion;

public class PromotionJobQueueTests
{
    [Fact]
    public void GivenEmptyQueue_WhenTryEnqueue_ShouldReturnTrue()
    {
        var sut = new PromotionJobQueue();

        sut.TryEnqueue(PromotionType.CtsBulk, Guid.NewGuid().ToString(), false, "user").Should().BeTrue();
    }

    [Fact]
    public void GivenJobQueued_WhenTryEnqueueAgain_ShouldReturnFalse()
    {
        var sut = new PromotionJobQueue();
        sut.TryEnqueue(PromotionType.CtsBulk, Guid.NewGuid().ToString(), false, "user");

        sut.TryEnqueue(PromotionType.CtsBulk, Guid.NewGuid().ToString(), false, "other").Should().BeFalse();
    }

    [Fact]
    public async Task GivenJobRunning_WhenTryEnqueue_ShouldReturnFalseUntilMarkedCompleted()
    {
        var sut = new PromotionJobQueue();
        sut.TryEnqueue(PromotionType.CtsBulk, Guid.NewGuid().ToString(), false, "user");

        await foreach (var job in sut.ReadAllAsync(TestContext.Current.CancellationToken))
        {
            job.RequestedBy.Should().Be("user");
            break;
        }

        sut.TryEnqueue(PromotionType.CtsBulk, Guid.NewGuid().ToString(), false, "other").Should().BeFalse();

        sut.MarkCompleted();

        sut.TryEnqueue(PromotionType.CtsBulk, Guid.NewGuid().ToString(), false, "other").Should().BeTrue();
    }
}