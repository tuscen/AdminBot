using Microsoft.Extensions.Options;

namespace AdminBot.Services.RateLimiting.Limiters
{
    public class TokenBucketLimiterFactory : IRateLimiterFactory
    {
        private readonly TokenBucketLimiterOptions _options;

        public TokenBucketLimiterFactory(IOptions<TokenBucketLimiterOptions> options)
        {
            _options = options.Value;
        }

        public IRateLimiter Create()
        {
            return new TokenBucketLimiter(_options.MaxBudget, _options.FillRatePerSec);
        }
    }
}
