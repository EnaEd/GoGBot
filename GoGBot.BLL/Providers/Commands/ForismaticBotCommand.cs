using GoGBot.BLL.Models;
using GoGBot.BLL.Providers.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Shared.Constants;
using System;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace GoGBot.BLL.Providers.Commands
{
    public class ForismaticBotCommand : IBotCommandProvider
    {
        private readonly INewsProvider _newsProvider;
        private readonly IBotClientProvider _botClientProvider;

        public string Name => Constant.BotMessageConstant.START_FORISMATIC_COMMAND;
        public ForismaticBotCommand(IServiceProvider serviceProvider, IBotClientProvider botClientProvider)
        {
            _newsProvider = serviceProvider.GetRequiredService<INewsProvider>();
            _botClientProvider = botClientProvider;
        }

        public async Task ExecuteAsync(Message message)
        {
            var client = await _botClientProvider.GetBotClientAsync();
            var responseContent = await GetContentModel();

            var chatId = message.Chat.Id;

            _ = responseContent.ContentType switch
            {
                Shared.Enums.Enum.ContentType.Text =>
                await client.SendTextMessageAsync(chatId, $"this is for {responseContent.Theme}\n {((ForismaticModel)responseContent.ContentData).QuoteText}\n\t\t\t{((ForismaticModel)responseContent.ContentData).QuoteAuthor}\n be happy dude)"),

                _ => await client.SendTextMessageAsync(chatId, $"this type hasn't implemented yet")
            };
        }

        public async Task<MessageContentModel> GetContentModel()
        {
            var theme = _newsProvider.GetCurrentTheme();
            var responseContent = await _newsProvider.GetCurrentNewsAsync(theme);//TODO EE: improve method for get different content by theme
            return new MessageContentModel
            {
                ContentData = responseContent,
                ContentType = Shared.Enums.Enum.ContentType.Text,//TODO EE: add dynamic receiving instead of hard code
                Theme = theme,
            };
        }
    }
}
