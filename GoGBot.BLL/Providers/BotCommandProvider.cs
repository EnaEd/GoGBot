using GoGBot.BLL.Models;
using GoGBot.BLL.Providers.Interfaces;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace GoGBot.BLL.Providers
{
    public class BotCommandProvider : IBotCommandProvider
    {
        public async Task ExecuteAsync(Message message, TelegramBotClient client, MessageContentModel messageContent)
        {
            var chatId = message.Chat.Id;

            _ = messageContent.ContentType switch
            {
                Shared.Enums.Enum.ContentType.Text =>
                await client.SendTextMessageAsync(chatId, $"this is for {messageContent.Theme}\n {((ForismaticModel)messageContent.ContentData).QuoteText}\n\t\t\t{((ForismaticModel)messageContent.ContentData).QuoteAuthor}\n be happy dude)"),

                _ => await client.SendTextMessageAsync(chatId, $"this type hasn't implemented yet")
            };

        }
    }
}
