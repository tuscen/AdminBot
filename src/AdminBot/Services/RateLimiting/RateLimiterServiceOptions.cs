using System.Collections.Concurrent;
using System.Collections.Generic;

namespace AdminBot.Services.RateLimiting
{
    public class RateLimiterServiceOptions
    {
        public IDictionary<int, IRateLimiter> Limiters { get; } = new ConcurrentDictionary<int, IRateLimiter>();
    }
}
