using System;
using System.Threading;
using System.Threading.Tasks;
using AdminBot.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Telegram.Bot.Framework.Abstractions;

namespace AdminBot
{
    public class BotService<TBot> : HostedService
        where TBot : class, IBot
    {
        private readonly IServiceScopeFactory _scopeFactory;

        private readonly ILogger _logger;

        public BotService(IServiceScopeFactory scopeFactory, ILogger<BotService<TBot>> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                try
                {
                    using (var scope = _scopeFactory.CreateScope())
                    {
                        var botManager = scope.ServiceProvider.GetService<IBotManager<TBot>>();
                        await botManager.GetAndHandleNewUpdatesAsync(timeout: 60);
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError(e.ToString());
                }
            }
        }
    }
}
