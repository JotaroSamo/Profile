using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace profile_API.Infastructure.HealthChecks
{
    public abstract class HttpResourceHealthCheck : IHealthCheck
    {
        private readonly ILogger _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _url;

        protected HttpResourceHealthCheck(ILogger logger,
            IHttpClientFactory httpClientFactory,
            string url)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _url = url;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var healthCheckResult = await CheckResource(cancellationToken);
                return healthCheckResult
                    ? HealthCheckResult.Healthy()
                    : new HealthCheckResult(context.Registration.FailureStatus);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while processing Http Resource Health Check. URL: {Url}", _url);
                return HealthCheckResult.Unhealthy();
            }
        }

        private async Task<bool> CheckResource(CancellationToken token)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync(_url, token);
            return response.IsSuccessStatusCode;
        }
    }
}