using AdminBot.Services.RateLimiting;
using System;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Framework;
using Telegram.Bot.Framework.Abstractions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace AdminBot.Bot
{
    public class RateLimiterHandler : UpdateHandlerBase
    {
        private readonly RateLimiterService _rateLimiterService;

        private static readonly ChatType[] AllowedChatTypes = { ChatType.Group, ChatType.Supergroup };

        public RateLimiterHandler(RateLimiterService rateLimiterService)
        {
            _rateLimiterService = rateLimiterService;
        }

        public override bool CanHandleUpdate(IBot bot, Update update)
            => update.Type == UpdateType.MessageUpdate &&
               AllowedChatTypes.Contains(update.Message.Chat.Type) &&
               !update.Message.From.IsBot;

        public override async Task<UpdateHandlingResult> HandleUpdateAsync(IBot bot, Update update)
        {
            var chatId = update.Message.Chat.Id;
            var userId = update.Message.From.Id;

            var chatAdministrators = await bot.Client.GetChatAdministratorsAsync(chatId);
            if (chatAdministrators.Any(member => member.User.Id == userId))
            {
                return UpdateHandlingResult.Continue;
            }

            var aquireSuccess = _rateLimiterService.TryAcquire(update.Message);

            if (!aquireSuccess)
            {
                await bot.Client.RestrictChatMemberAsync(chatId, userId, DateTime.UtcNow + TimeSpan.FromMinutes(1), false);
            }

            return UpdateHandlingResult.Continue;
        }
    }
}
