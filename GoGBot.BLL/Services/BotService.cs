﻿using GoGBot.BLL.Providers.Commands;
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

namespace GoGBot.BLL.Services
{
    public class BotService : IBotService
    {
        private TelegramBotClient _botClient;

        private readonly IConfiguration _configuration;
        private readonly INewsProvider _newsProvider;
        private readonly IServiceProvider _serviceProvider;


        public BotService(IConfiguration configuration, INewsProvider newsProvider, IServiceProvider serviceProvider)
        {
            _configuration = configuration;
            _newsProvider = newsProvider;
            _serviceProvider = serviceProvider;
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

            await _botClient.SetWebhookAsync("https://53bb4a1460a3.ngrok.io/api/message/update");


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
            if (update.Message.Text.Equals(Constant.BotMessageConstant.START_FORISMATIC_COMMAND))
            {
                RecurringJob.AddOrUpdate<ForismaticBotCommand>(nameof(ForismaticBotCommand),
                    (command) => command.ExecuteAsync(update.Message), Cron.Daily);
            }
            if (update.Message.Text.Equals(Constant.BotMessageConstant.START_COLLECT_COMMAND))
            {
                RecurringJob.AddOrUpdate<CollectBotCommand>(nameof(CollectBotCommand),
                    (command) => command.ExecuteAsync(update.Message), Cron.Monthly(29));
            }

        }
    }
}
