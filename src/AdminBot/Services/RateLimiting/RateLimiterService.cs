using System.Collections.Generic;
using System.Collections.Concurrent;
using Microsoft.Extensions.Options;
using Telegram.Bot.Types;

namespace AdminBot.Services.RateLimiting
{
    public class RateLimiterService
    {
        private IDictionary<int, IRateLimiter> Limiters { get; } = new ConcurrentDictionary<int, IRateLimiter>();

        private readonly IRateLimiterFactory _limiterFactory;

        public RateLimiterService(IRateLimiterFactory limiterFactory)
        {
            _limiterFactory = limiterFactory;
        }

        public bool TryAcquire(Message message)
        {
            var userId = message.From.Id;
            if (!Limiters.TryGetValue(userId, out var limiter))
            {
                limiter = _limiterFactory.Create();
                Limiters.TryAdd(userId, limiter);
            }

            return limiter.TryAcquire(1);
        }
    }
}
