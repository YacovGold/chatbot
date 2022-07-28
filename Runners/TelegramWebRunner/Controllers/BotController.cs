using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dals;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramWebRunner.Controllers
{
    [ApiController]
    [Route("")]
    public class BotController : ControllerBase
    {
        private static PluginExecutor pluginExecutor = new PluginExecutor(new MemoryDal(), new PluginsMenu(), new PluginsManager());
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Content("Hello");
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Update update)
        {
            var value = Environment.GetEnvironmentVariable("TelegramKey");
            var client = new TelegramBotClient(value);

            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {
                var res = pluginExecutor.Run(update.Message.Text, update.Message.Chat.Id.ToString());
                var action = client.SendTextMessageAsync(chatId: update.Message.Chat.Id, res);
                Message sentMessage = await action;
            }

            return Ok();
        }

    }
}
