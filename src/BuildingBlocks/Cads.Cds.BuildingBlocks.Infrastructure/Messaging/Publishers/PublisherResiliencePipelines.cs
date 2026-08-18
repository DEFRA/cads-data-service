using Cads.Cds.BuildingBlocks.Core.Exceptions;
using Polly;
using Polly.Retry;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Messaging.Publishers;

public static class PublisherResiliencePipelines
{
    public static ResiliencePipeline CreateDefaultQueueRetryPipeline(int maxRetryAttempts = 3)
    {
        return new ResiliencePipelineBuilder()
            .AddRetry(new RetryStrategyOptions
            {
                ShouldHandle = new PredicateBuilder()
                    .Handle<PublishFailedException>(ex => ex.IsTransient),
                MaxRetryAttempts = maxRetryAttempts,
                BackoffType = DelayBackoffType.Exponential,
                Delay = TimeSpan.FromSeconds(1) // 1s, 2s, 4s
            })
            .Build();
    }
}