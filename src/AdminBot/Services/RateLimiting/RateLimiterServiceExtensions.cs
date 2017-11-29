using Microsoft.Extensions.DependencyInjection;

namespace AdminBot.Services.RateLimiting
{
    public static class RateLimiterServiceExtensions
    {
        public static IServiceCollection AddRateLimiterService(this IServiceCollection services)
        {
            services.Configure<RateLimiterServiceOptions>(options => { });
            services.AddTransient<RateLimiterService>();
            return services;
        }
    }
}
