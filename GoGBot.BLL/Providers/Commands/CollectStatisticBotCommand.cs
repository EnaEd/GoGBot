using GoGBot.BLL.Providers.Interfaces;
using GoGBot.BLL.Services.Interfaces;
using Shared.Constants;
using System;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using static Shared.Enums.Enum;

namespace GoGBot.BLL.Providers.Commands
{
    public class CollectStatisticBotCommand : IBotCommandProvider
    {
        private readonly IElectorService _electorService;
        private readonly IBotClientProvider _botClientProvider;

        public CollectStatisticBotCommand(IElectorService electorService, IBotClientProvider botClientProvider)
        {
            _electorService = electorService;
            _botClientProvider = botClientProvider;
        }

        public string Name => Constant.BotMessageConstant.COLLECT_STAT_COMMAND;

        public async Task ExecuteAsync(Message message)
        {
            var client = await _botClientProvider.GetBotClientAsync();
            var chatId = message.Chat.Id;

            string resultMessage = GenerateResultMessage();

            await client.SendTextMessageAsync(chatId, resultMessage);

            _electorService.IsCanVote = false;//end vote period
        }

        private string GenerateResultMessage()
        {
            StringBuilder builder = new();
            builder.Append("result :\n");

            _electorService.ElectorsTeam.ForEach(item =>
                builder.Append($"{item.NickName}\t\t\t +\n"));

            builder.Append($"\t\t\tSo {_electorService.ElectorsTeam.Count} vs {(Enum.GetValues(typeof(TeamPlayersType)).Length - 1) - _electorService.ElectorsTeam.Count}");

            return builder.ToString();
        }
    }
}
