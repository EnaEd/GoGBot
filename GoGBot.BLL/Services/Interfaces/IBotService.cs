using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace GoGBot.BLL.Services.Interfaces
{
    public interface IBotService
    {
        public Task<TelegramBotClient> GetBotClientAsync();
        public Task SetupAndExecuteAsync(Update update);
    }
}
