using Dals;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;
using Telegram.Bot.Types;
using BasePlugin.Interfaces;
using Services;

namespace WebRunner.Controllers
{
    [ApiController]
    [Route("telegram")]
    public class TelegramBotController : ControllerBase
    {
        private static PluginExecutor _pluginExecutor;

        public TelegramBotController(PluginExecutor pluginExecutor)
        {
            _pluginExecutor = pluginExecutor;
        }

        [HttpGet] public IActionResult Get() => Content("Hello");

        [HttpPost]
        public IActionResult Post([FromBody] Update update)
        {
            
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {
                BasePlugin.Records.User user = new BasePlugin.Records.User(update.Message.Chat.Id.ToString(), RunnerType.Telegram);
                _pluginExecutor.Run(update.Message.Text, user);
            }

            return Ok();
        }
    }
}
