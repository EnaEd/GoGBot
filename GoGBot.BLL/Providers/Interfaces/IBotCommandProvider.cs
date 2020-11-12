using GoGBot.BLL.Models;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace GoGBot.BLL.Providers.Interfaces
{
    public interface IBotCommandProvider
    {
        public string Name => @"/start";
        public Task ExecuteAsync(Message message, TelegramBotClient client, MessageContentModel messageContent);
        public bool Contains(Message message) =>
            message.Type != Telegram.Bot.Types.Enums.MessageType.Text && message.Text.Contains(Name);
    }
}
