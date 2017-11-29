using System;
using Microsoft.Extensions.Options;

namespace AdminBot.Services.RateLimiting
{
    public class SimpleRateLimiterFactory : IRateLimiterFactory
    {
        private readonly SimpleRateLimiterOptions _options;

        public SimpleRateLimiterFactory(IOptions<SimpleRateLimiterOptions> options)
        {
            _options = options.Value;
        }

        public IRateLimiter Create()
        {
            return new SimpleRateLimiter(_options.MaxBudget, TimeSpan.FromSeconds(_options.TimeWindow));
        }
    }
}
