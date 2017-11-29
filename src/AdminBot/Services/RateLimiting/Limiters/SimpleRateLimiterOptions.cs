namespace AdminBot.Services.RateLimiting
{
    public class SimpleRateLimiterOptions
    {
        /// <summary>
        /// Maximum number of actions per time window
        /// </summary>
        public int MaxBudget { get; set; } = 10;

        /// <summary>
        ///
        /// </summary>
        public int TimeWindow { get; set; } = 60;
    }
}
