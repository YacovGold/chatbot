using Dals;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;
using Telegram.Bot.Types;
using BasePlugin.Interfaces;
using System.Globalization;

namespace TelegramWebRunner.Controllers
{
    [ApiController]
    [Route("telegram")]
    public class BotController : ControllerBase, IMessageSender
    {
        private static PluginExecutor pluginExecutor;
        private readonly ITelegramBotClient client;

        public BotController()
        {
            pluginExecutor ??= new PluginExecutor(this, new DbDal(), new PluginsMenu(), new PluginsManager());
            var value = Environment.GetEnvironmentVariable("TelegramKey");
            client = new TelegramBotClient(value);

            
        }

        [HttpGet] public IActionResult Get() => Content("Hello");

        [HttpPost]
        public IActionResult Post([FromBody] Update update)
        {
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {
                Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture = new CultureInfo("da-DK");
                var dt = DateTime.Parse(DateTime.Now.ToString(), Thread.CurrentThread.CurrentCulture);
                Console.WriteLine(dt);
                pluginExecutor.Run(update.Message.Text, update.Message.Chat.Id.ToString());
            }

            return Ok();
        }

        public async void SendMessage(string userId, string data)
        {
            var chatId = long.Parse(userId);
            await client.SendTextMessageAsync(chatId: chatId, data);
        }
    }
}
