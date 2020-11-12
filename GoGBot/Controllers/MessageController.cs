using GoGBot.BLL.Services.Interfaces;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Shared.Constants;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace GoGBot.Controllers
{
    [Route(Constant.Routes.MESSAGE_CONTROLLER)]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IBotService _botService;

        public MessageController(IBotService botService)
        {
            _botService = botService;
        }
        //private readonly TelegramBotClient client = new TelegramBotClient("1406351812:AAFa98XPkV14v292oWR4z-pZC_Ft2t5niqY");

        [HttpPost(Constant.Routes.MESSAGE_UPDATE_ROUTE)]
        public async Task<IActionResult> Post([FromBody] Update update)
        {

            RecurringJob.AddOrUpdate(() => _botService.ExecuteIfCanAsync(update), Cron.Daily);

            return Ok();
        }
    }
}
