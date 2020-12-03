using System.Threading.Tasks;
using Telegram.Bot;

namespace GoGBot.BLL.Providers.Interfaces
{
    public interface IBotClientProvider
    {
        public TelegramBotClient BotClient { get; set; }
        public Task<TelegramBotClient> GetBotClientAsync();
    }
}
