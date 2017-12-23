using System;

namespace AdminBot.Services.RateLimiting.Limiters
{
    public class TokenBucketLimiter : IRateLimiter
    {
        private readonly long _fillRatePerSec;

        private readonly long _maxBudget;

        private long _currentBudget;

        private long _lastUpdateTime;

        public TokenBucketLimiter(long maxBudget, long fillRatePerSec)
        {
            _fillRatePerSec = fillRatePerSec;
            _maxBudget      = maxBudget;
            _currentBudget  = maxBudget;
            _lastUpdateTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        }

        /// <inheritdoc />
        public bool TryAcquire(int amount)
        {
            var secSinceLastUpdate = DateTimeOffset.UtcNow.ToUnixTimeSeconds() - _lastUpdateTime;
            _currentBudget = Math.Min(_maxBudget, _currentBudget + secSinceLastUpdate * _fillRatePerSec);
            _lastUpdateTime += secSinceLastUpdate;

            if (_currentBudget >= amount)
            {
                _currentBudget -= amount;
                return true;
            }
            return false;
        }
    }
}
