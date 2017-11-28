namespace AdminBot.Services.RateLimiting
{
    public interface IRateLimiter
    {
        /// <summary>
        /// Attempt to consume the specified amount of resources.  If the resources
        /// are available, consume them and return true; otherwise, consume nothing
        /// and return false.
        /// </summary>
        /// <param name="amount">amount of resource to consume</param>
        /// <returns></returns>
        bool TryAcquire(int amount);
    }
}
