using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AdminBot.Services.RateLimiting.Limiters
{
    public static class SimpleRateLimiterExtentions
    {
        public static IServiceCollection AddSimpleRateLimiter(
            this IServiceCollection services,
            SimpleRateLimiterOptions options)
        {
            services.Configure<SimpleRateLimiterOptions>(opts =>
            {
                opts.TimeWindow = options.TimeWindow;
                opts.MaxBudget = options.MaxBudget;
            });

            services.AddTransient<IRateLimiterFactory, SimpleRateLimiterFactory>();

            return services;
        }

        public static IServiceCollection AddSimpleRateLimiter(
            this IServiceCollection services,
            Action<SimpleRateLimiterOptions> configure)
        {
            services.Configure(configure);
            services.AddTransient<IRateLimiterFactory, SimpleRateLimiterFactory>();

            return services;
        }

        public static IServiceCollection AddSimpleRateLimiter(
            this IServiceCollection services,
            IConfiguration configure)
        {
            services.Configure<SimpleRateLimiterOptions>(configure);
            services.AddTransient<IRateLimiterFactory, SimpleRateLimiterFactory>();

            return services;
        }
    }
}
