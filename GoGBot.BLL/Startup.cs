using GoGBot.BLL.Providers;
using GoGBot.BLL.Providers.Interfaces;
using GoGBot.BLL.Services;
using GoGBot.BLL.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace GoGBot.BLL
{
    public static class Startup
    {
        public static void InitStartup(this IServiceCollection services)
        {
            services.AddTransient<IBotCommandProvider, BotCommandProvider>();
            services.AddTransient<IBotService, BotService>();
            services.AddTransient<INewsProvider, NewsProvider>();
            services.AddTransient<IRandomProvider, RandomProvider>();
        }
    }
}
