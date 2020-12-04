using GoGBot.BLL.Models;
using GoGBot.BLL.Providers.Interfaces;
using GoGBot.BLL.Services.Interfaces;
using Shared.Constants;
using Shared.Extension;
using System;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using static Shared.Enums.Enum;

namespace GoGBot.BLL.Providers.Commands
{
    public class HandleTeamBotCommand : IBotCommandProvider
    {
        private readonly IElectorService _electorService;
        private readonly IBotClientProvider _botClientProvider;
        public HandleTeamBotCommand(IElectorService electorService, IBotClientProvider botClientProvider)
        {
            _electorService = electorService;
            _botClientProvider = botClientProvider;
        }

        public string Name => $"{Constant.BotMessageConstant.FOLLOW_ACCEPT_COMMAND} / {Constant.BotMessageConstant.FOLLOW_DENIED_COMMAND}";

        public async Task ExecuteAsync(Message message)
        {

            if (!_electorService.IsCanVote)
            {
                await OnAccessDenied(message);
                return;
            }

            if (message.Text.Equals(Constant.BotMessageConstant.FOLLOW_ACCEPT_COMMAND) &&
                    !_electorService.ElectorsTeam.Any(item => item.Id == message.From.Id) &&
                    Enum.IsDefined(typeof(TeamPlayersType), message.From.Id))
            {
                var electorType = (TeamPlayersType)Enum.Parse(typeof(TeamPlayersType), message.From.Id.ToString());

                Elector elector = new();
                elector.Id = message.From.Id;
                elector.FirstName = electorType.GetAttribute<EnumDescriptor>().FirstName;
                elector.LastName = electorType.GetAttribute<EnumDescriptor>().LastName;

                _electorService.ElectorsTeam.Add(elector);
                await OnSaveVote(message);
                return;
            }

            if (message.Text.Equals(Constant.BotMessageConstant.FOLLOW_DENIED_COMMAND) &&
                    _electorService.ElectorsTeam.Any(item => item.Id == message.From.Id) &&
                    Enum.IsDefined(typeof(TeamPlayersType), message.From.Id))
            {
                _electorService.ElectorsTeam.RemoveAll(item => item.Id == message.From.Id);
                await OnSaveVote(message);
                return;
            }
            await OnAccessDenied(message);
        }

        private async Task OnAccessDenied(Message message)
        {
            var client = await _botClientProvider.GetBotClientAsync();
            var chatId = message.Chat.Id;

            await client.SendTextMessageAsync(chatId, $"sorry dude but you don't have access,please call to the admin");
        }

        private async Task OnSaveVote(Message message)
        {
            var client = await _botClientProvider.GetBotClientAsync();
            var chatId = message.Chat.Id;

            await client.SendTextMessageAsync(chatId, $"ur voice is recorded");

        }

    }
}
