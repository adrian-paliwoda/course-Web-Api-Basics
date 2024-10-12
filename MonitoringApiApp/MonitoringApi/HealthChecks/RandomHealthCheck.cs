using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace MonitoringApi.HealthChecks;

public class RandomHealthCheck : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
    {
        int responseTimeInMilliseconds = Random.Shared.Next(300);

        if (responseTimeInMilliseconds < 100)
        {
            return Task.FromResult(HealthCheckResult.Healthy(
                $"The response time is excellent ({responseTimeInMilliseconds}ms)"));
        }

        if (responseTimeInMilliseconds < 200)
        {
            return Task.FromResult(HealthCheckResult.Degraded(
                $"The response time is greater than expected ({responseTimeInMilliseconds}ms)"));
        }

        return Task.FromResult(HealthCheckResult.Unhealthy(
            $"The response time is unacceptable ({responseTimeInMilliseconds}ms)"));

    }
}