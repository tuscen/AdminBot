namespace AdminBot.Services.RateLimiting
{
    public interface IRateLimiterFactory
    {
        IRateLimiter Create();
    }
}
