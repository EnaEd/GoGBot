using GoGBot.BLL.Providers.Interfaces;
using Shared.Constants;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace GoGBot.BLL.Providers.Commands
{
    public class InfoBotCommand : IBotCommandProvider
    {
        private readonly IBotClientProvider _botClientProvider;

        public InfoBotCommand(IBotClientProvider botClientProvider)
        {
            _botClientProvider = botClientProvider;
        }

        public string Name => Constant.BotMessageConstant.HELP_COMMAND;

        public async Task ExecuteAsync(Message message)
        {
            var client = await _botClientProvider.GetBotClientAsync();


            var chatId = message.Chat.Id;


            await client.SendTextMessageAsync(chatId, $"these are the commands you could use\n" +
                $"\t\t\t{Constant.BotMessageConstant.HELP_COMMAND} - get info about commands\n" +
                $"\t\t\t{Constant.BotMessageConstant.FOLLOW_ACCEPT_COMMAND} - to confirm to come on to movie's party\n" +
                $"\t\t\t{Constant.BotMessageConstant.FOLLOW_DENIED_COMMAND} - to decline movie's party");



        }
    }
}
