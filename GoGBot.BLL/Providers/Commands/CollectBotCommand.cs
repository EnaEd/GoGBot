using GoGBot.BLL.Providers.Interfaces;
using GoGBot.BLL.Services.Interfaces;
using Shared.Constants;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace GoGBot.BLL.Providers.Commands
{
    public class CollectBotCommand : IBotCommandProvider
    {
        private readonly IBotClientProvider _botClientProvider;
        private readonly IElectorService _electorService;
        public CollectBotCommand(IBotClientProvider botClientProvider, IElectorService electorService)
        {
            _botClientProvider = botClientProvider;
            _electorService = electorService;
        }
        public string Name => Constant.BotMessageConstant.START_COLLECT_COMMAND;

        public async Task ExecuteAsync(Message message)
        {
            var client = await _botClientProvider.GetBotClientAsync();
            var chatId = message.Chat.Id;
            await client.SendTextMessageAsync(chatId, $"let's go vote who going to movie's party");
            //for possibility  vote in collect period
            _electorService.IsCanVote = true;
        }

        public async Task SendMessage(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            await client.SendTextMessageAsync(chatId, $"let's go vote who going to movie's party");
        }
    }
}
