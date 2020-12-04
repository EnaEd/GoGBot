using GoGBot.BLL.Providers.Interfaces;
using GoGBot.BLL.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Shared.Constants;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace GoGBot.BLL.Providers.Commands
{
    public class IQGameBotCommand : IBotCommandProvider
    {
        private readonly IConfiguration _configuration;
        private readonly IBotClientProvider _botClientProvider;
        private readonly IElectorService _electorService;
        public IQGameBotCommand(IConfiguration configuration, IBotClientProvider botClientProvider, IElectorService electorService)
        {
            _configuration = configuration;
            _botClientProvider = botClientProvider;
            _electorService = electorService;
        }

        public string Name => Constant.BotMessageConstant.IQ_GAME_COMMAND;

        public async Task ExecuteAsync(Message message)
        {
            var client = await _botClientProvider.GetBotClientAsync();



            var chatId = message.Chat.Id;
            IEnumerable<IEnumerable<InlineKeyboardButton>> buttons = CreateKeyboard();

            var keyboard = new InlineKeyboardMarkup(buttons);

            await client.SendTextMessageAsync(chatId, $"let's play", Telegram.Bot.Types.Enums.ParseMode.Default, false, false, 0, keyboard);

        }


        private IEnumerable<IEnumerable<InlineKeyboardButton>> CreateKeyboard()
        {
            List<InlineKeyboardButton> buttonRow1 = new();
            buttonRow1.Add(new InlineKeyboardButton { Text = "one", CallbackData = "ping one" });
            buttonRow1.Add(new InlineKeyboardButton { Text = "two", CallbackData = "ping two" });
            List<InlineKeyboardButton> buttonRow2 = new();
            buttonRow1.Add(new InlineKeyboardButton { Text = "three", CallbackData = "ping three" });
            buttonRow1.Add(new InlineKeyboardButton { Text = "four", CallbackData = "ping four" });
            List<List<InlineKeyboardButton>> buttonList = new();
            buttonList.Add(buttonRow1);
            buttonList.Add(buttonRow2);
            return buttonList;
        }
    }
}
