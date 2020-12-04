using GoGBot.BLL.Providers.Interfaces;
using Microsoft.Extensions.Configuration;
using Shared.Configurations;
using Shared.Constants;
using System.Threading.Tasks;
using Telegram.Bot;

namespace GoGBot.BLL.Providers
{
    public class BotClientProvider : IBotClientProvider
    {
        private readonly IConfiguration _configuration;

        public BotClientProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public TelegramBotClient BotClient { get; set; }


        public async Task<TelegramBotClient> GetBotClientAsync()
        {
            if (BotClient is not null)
            {
                return BotClient;
            }

            BotClient = new TelegramBotClient(
            _configuration[$"{nameof(Configuratins.BotSettings)}:{nameof(Configuratins.BotSettings.Key)}"]);

            //For debug by ngrok

            //await BotClient.SetWebhookAsync("https://30afdd1f9ed5.ngrok.io/api/message/update");


            string hookUrl = $"{_configuration[$"{nameof(Configuratins.BotSettings)}:{nameof(Configuratins.BotSettings.AppUrl)}"]}";
            string hookRoute = $"{Constant.Routes.MESSAGE_CONTROLLER}/{Constant.Routes.MESSAGE_UPDATE_ROUTE}";

            await BotClient.SetWebhookAsync($"{hookUrl}{hookRoute}");

            return BotClient;
        }
    }
}
