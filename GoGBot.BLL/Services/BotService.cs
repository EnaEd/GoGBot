using GoGBot.BLL.Providers.Commands;
using GoGBot.BLL.Providers.Interfaces;
using GoGBot.BLL.Services.Interfaces;
using Hangfire;
using Microsoft.Extensions.Configuration;
using Shared.Configurations;
using Shared.Constants;
using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using static Shared.Enums.Enum;

namespace GoGBot.BLL.Services
{
    public class BotService : IBotService
    {
        private TelegramBotClient _botClient;

        private readonly IConfiguration _configuration;
        private readonly INewsProvider _newsProvider;
        private readonly IServiceProvider _serviceProvider;
        private readonly IBotCommandProvider _botCommandProvider;
        private readonly IBotClientProvider _botClientProvider;



        public BotService(IConfiguration configuration, INewsProvider newsProvider, IServiceProvider serviceProvider, IBotCommandProvider botCommandProvider, IBotClientProvider botClientProvider)
        {
            _configuration = configuration;
            _newsProvider = newsProvider;
            _serviceProvider = serviceProvider;
            _botCommandProvider = botCommandProvider;
            _botClientProvider = botClientProvider;
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

            await _botClient.SetWebhookAsync("https://30afdd1f9ed5.ngrok.io/api/message/update");


            //string hookUrl = $"{_configuration[$"{nameof(Configuratins.BotSettings)}:{nameof(Configuratins.BotSettings.AppUrl)}"]}";
            //string hookRoute = $"{Constant.Routes.MESSAGE_CONTROLLER}/{Constant.Routes.MESSAGE_UPDATE_ROUTE}";

            //await _botClient.SetWebhookAsync($"{hookUrl}{hookRoute}");

            return _botClient;

        }

        public async Task SetupAndExecuteAsync(Update update)
        {
            if (update is null)
            {
                return;
            }
            if (update.Type is UpdateType.CallbackQuery)
            {
                _botClient = await _botClientProvider.GetBotClientAsync();
                _botClient.OnCallbackQuery += async (object sender, Telegram.Bot.Args.CallbackQueryEventArgs e) =>
                {

                    var message = e.CallbackQuery.Message;
                    if (e.CallbackQuery.Data == "one")
                    {
                        await _botClient.AnswerCallbackQueryAsync(e.CallbackQuery.Id, "You hav choosen " + e.CallbackQuery.Data, true);
                    }

                    await _botClient.SendTextMessageAsync(message.Chat.Id, "тест", replyToMessageId: message.MessageId);
                    await _botClient.AnswerCallbackQueryAsync(e.CallbackQuery.Id); // отсылаем пустое, чтобы убрать "частики" на кнопке

                };
                return;
            }
            if (update.Message.Text.Equals(Constant.BotMessageConstant.START_FORISMATIC_COMMAND)
                && update.Message.From.Id == (int)TeamPlayersType.Admin)
            {
                RecurringJob.AddOrUpdate<ForismaticBotCommand>(nameof(ForismaticBotCommand),
                    (command) => command.ExecuteAsync(update.Message), Cron.Daily);
            }
            if (update.Message.Text.Equals(Constant.BotMessageConstant.START_COLLECT_COMMAND)
                && update.Message.From.Id == (int)TeamPlayersType.Admin)
            {
                RecurringJob.AddOrUpdate<CollectBotCommand>(nameof(CollectBotCommand),
                    (command) => command.ExecuteAsync(update.Message), Cron.Monthly(28));
            }
            if (update.Message.Text.Equals(Constant.BotMessageConstant.HELP_COMMAND))
            {
                var provider = _botCommandProvider as InfoBotCommand;
                await provider.ExecuteAsync(update.Message);
            }
            if (update.Message.Text.Equals(Constant.BotMessageConstant.FOLLOW_ACCEPT_COMMAND) ||
                update.Message.Text.Equals(Constant.BotMessageConstant.FOLLOW_DENIED_COMMAND))
            {
                //var provider = _botCommandProvider as HandleTeamBotCommand;
                var provider = _serviceProvider.GetService(typeof(HandleTeamBotCommand)) as HandleTeamBotCommand;
                await provider.ExecuteAsync(update.Message);
            }
            if (update.Message.Text.Equals(Constant.BotMessageConstant.COLLECT_STAT_COMMAND)
                && update.Message.From.Id == (int)TeamPlayersType.Admin)
            {
                RecurringJob.AddOrUpdate<CollectStatisticBotCommand>(nameof(CollectStatisticBotCommand),
                    (command) => command.ExecuteAsync(update.Message), Cron.Monthly(1));
            }

            if (update.Message.Text.Equals(Constant.BotMessageConstant.IQ_GAME_COMMAND)
                && update.Message.From.Id == (int)TeamPlayersType.Admin)
            {
                RecurringJob.AddOrUpdate<IQGameBotCommand>(nameof(IQGameBotCommand),
                    (command) => command.ExecuteAsync(update.Message), Cron.Monthly(1, 10));
            }


        }
    }
}
