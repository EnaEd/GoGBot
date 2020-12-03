using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace GoGBot.BLL.Providers.Interfaces
{
    public interface IBotCommandProvider
    {
        public string Name { get; }
        public Task ExecuteAsync(Message message);

    }
}
