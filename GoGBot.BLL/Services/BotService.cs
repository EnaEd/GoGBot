using GoGBot.BLL.Models;
using GoGBot.BLL.Providers.Interfaces;
using GoGBot.BLL.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Shared.Configurations;
using Shared.Constants;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace GoGBot.BLL.Services
{
    public class BotService : IBotService
    {
        private TelegramBotClient _botClient;
        private IBotCommandProvider _botCommandProvider;
        private readonly IConfiguration _configuration;
        private readonly INewsProvider _newsProvider;

        public BotService(IBotCommandProvider botCommandProvider, IConfiguration configuration, INewsProvider newsProvider)
        {
            _botCommandProvider = botCommandProvider;
            _configuration = configuration;
            _newsProvider = newsProvider;
        }

        public async Task<TelegramBotClient> GetBotClientAsync()
        {
            if (_botClient is not null)
            {
                return _botClient;
            }

            _botClient = new TelegramBotClient(
                _configuration[$"{nameof(Configuratins.BotSettings)}:{nameof(Configuratins.BotSettings.Key)}"]);

            //For debug by ngrok

            //await _botClient.SetWebhookAsync("https://269b6b09f10f.ngrok.io/api/message/update");


            string hookUrl = $"{_configuration[$"{nameof(Configuratins.BotSettings)}:{nameof(Configuratins.BotSettings.AppUrl)}"]}";
            string hookRoute = $"{Constant.Routes.MESSAGE_CONTROLLER}/{Constant.Routes.MESSAGE_UPDATE_ROUTE}";

            await _botClient.SetWebhookAsync($"{hookUrl}{hookRoute}");

            return _botClient;

        }

        public async Task ExecuteIfCanAsync(Update update)
        {
            if (update is null)
            {
                return;
            }
            var client = await GetBotClientAsync();
            var responseContent = await GetContentModel();
            await _botCommandProvider.ExecuteAsync(update.Message, client, responseContent);
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
