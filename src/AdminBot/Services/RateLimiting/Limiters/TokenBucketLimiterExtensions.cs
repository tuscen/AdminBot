using System;
using Microsoft.Extensions.DependencyInjection;

namespace AdminBot.Services.RateLimiting.Limiters
{
    public static class TokenBucketLimiterExtensions
    {
        public static IServiceCollection AddTokenBucketLimiter(
            this IServiceCollection services,
            TokenBucketLimiterOptions options)
        {
            services.Configure<TokenBucketLimiterOptions>(opts =>
            {
                opts.FillRatePerSec = options.FillRatePerSec;
                opts.MaxBudget = options.MaxBudget;
            });

            services.AddTransient<IRateLimiterFactory, TokenBucketLimiterFactory>();

            return services;
        }

        public static IServiceCollection AddTokenBucketLimiter(
            this IServiceCollection services,
            Action<TokenBucketLimiterOptions> configure)
        {
            services.Configure(configure);
            services.AddTransient<IRateLimiterFactory, TokenBucketLimiterFactory>();

            return services;
        }
    }
}
