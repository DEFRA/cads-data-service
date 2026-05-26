namespace Cads.Cds.BuildingBlocks.Testing.Support.Utilities.CircuitBreakers;

public static class CircuitBreakerUtilities
{
    public static async Task<T> WaitUntilAsync<T>(
        Func<Task<T>> action,
        Func<T, bool> condition,
        TimeSpan timeout,
        TimeSpan pollInterval)
    {
        var start = DateTime.UtcNow;

        while (DateTime.UtcNow - start < timeout)
        {
            var result = await action();
            if (condition(result))
                return result;

            await Task.Delay(pollInterval);
        }

        throw new TimeoutException("Condition not met within timeout.");
    }
}