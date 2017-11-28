using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Telegram.Bot.Framework;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace AdminBot.Bot
{
    public class AdminBot : BotBase<AdminBot>
    {
        private const string UnknownUpdateText = "Sorry! I don't know what to do with this message";

        private readonly ILogger _logger;

        private readonly BotOptions<AdminBot> _options;

        public AdminBot(IOptions<BotOptions<AdminBot>> botOptions, ILogger<AdminBot> logger) : base(botOptions)
        {
            _options = botOptions.Value;
            _logger = logger;
        }

        public override async Task HandleUnknownUpdate(Update update)
        {
            _logger.LogWarning("Unable to handle an update");

            if (update.Type == UpdateType.MessageUpdate)
            {
                await Client.SendTextMessageAsync(update.Message.Chat.Id,
                    UnknownUpdateText,
                    replyToMessageId: update.Message.MessageId);
            }
        }

        public override Task HandleFaultedUpdate(Update update, Exception e)
        {
            _logger.LogCritical("Exception thrown while handling an update");
            return Task.CompletedTask;
        }
    }
}
