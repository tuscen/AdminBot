namespace AdminBot.Services.RateLimiting.Limiters
{
    public class TokenBucketLimiterOptions
    {
        /// <summary>
        /// Maximum number of actions per time window
        /// </summary>
        public int MaxBudget { get; set; } = 10;

        /// <summary>
        ///
        /// </summary>
        public int FillRatePerSec { get; set; } = 60;
    }
}
