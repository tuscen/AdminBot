using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Telegram.Bot.Types;

namespace AdminBot.Services.RateLimiting
{
    public class RateLimiterService
    {
        private IDictionary<int, IRateLimiter> Limiters { get; } = new ConcurrentDictionary<int, IRateLimiter>();

        public bool TryAcquire(Message message)
        {
            var userId = message.From.Id;
            if (!Limiters.TryGetValue(userId, out var limiter))
            {
                limiter = new SimpleRateLimiter(2, TimeSpan.FromSeconds(10));
                Limiters.TryAdd(userId, limiter);
            }

            return limiter.TryAcquire(1);
        }
    }
}
