using System.Collections.Generic;
using Microsoft.Extensions.Options;
using Telegram.Bot.Types;

namespace AdminBot.Services.RateLimiting
{
    public class RateLimiterService
    {
        private readonly IRateLimiterFactory _limiterFactory;

        private readonly RateLimiterServiceOptions _options;

        public RateLimiterService(IOptions<RateLimiterServiceOptions> options, IRateLimiterFactory limiterFactory)
        {
            _options = options.Value;
            _limiterFactory = limiterFactory;
        }

        public bool TryAcquire(Message message)
        {
            var userId = message.From.Id;
            if (!_options.Limiters.TryGetValue(userId, out var limiter))
            {
                limiter = _limiterFactory.Create();
                _options.Limiters.TryAdd(userId, limiter);
            }

            return limiter.TryAcquire(1);
        }
    }
}
