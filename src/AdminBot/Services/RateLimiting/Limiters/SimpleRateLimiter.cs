using System;

namespace AdminBot.Services.RateLimiting
{
    /// <summary>
    /// Simple rate limiter that doesn't allow any bursts within a time window.
    /// </summary>
    public class SimpleRateLimiter : IRateLimiter
    {
        private readonly int _maxBudget;

        private int _currentBudget;

        private readonly TimeSpan _timeWindow;

        private DateTime _timeWindowStart;

        public SimpleRateLimiter(int maxBudget, TimeSpan timeWindow)
        {
            _maxBudget = maxBudget;
            _timeWindow = timeWindow;
            _timeWindowStart = DateTime.UtcNow;
        }

        /// <inheritdoc />
        public bool TryAcquire(int amount)
        {
            var now = DateTime.UtcNow;
            var diff = now - _timeWindowStart;

            if (diff > _timeWindow)
            {
                Reset();
            }

            _currentBudget += amount;
            return _currentBudget <= _maxBudget;
        }

        private void Reset()
        {
            _currentBudget = 0;
            _timeWindowStart = DateTime.UtcNow;
        }
    }
}
