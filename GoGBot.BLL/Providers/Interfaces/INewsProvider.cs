using GoGBot.BLL.Models;
using System.Threading.Tasks;

namespace GoGBot.BLL.Providers.Interfaces
{
    public interface INewsProvider
    {
        public Shared.Enums.Enum.NewsThemeType GetCurrentTheme();
        public Task<ForismaticModel> GetCurrentNewsAsync(Shared.Enums.Enum.NewsThemeType theme);
    }
}
