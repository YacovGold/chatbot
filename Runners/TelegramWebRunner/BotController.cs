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
using BasePlugin.Interfaces;

namespace TelegramWebRunner.Controllers
{
    [ApiController]
    [Route("")]
    public class BotController : ControllerBase, IMessageSender
    {
        private static PluginExecutor pluginExecutor;
        private readonly ITelegramBotClient client;
        public BotController()
        {
            pluginExecutor ??= new PluginExecutor(this, new MemoryDal(), new PluginsMenu(), new PluginsManager());
            var value = Environment.GetEnvironmentVariable("TelegramKey");
            client = new TelegramBotClient(value);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Content("Hello");
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Update update)
        {
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {
                pluginExecutor.Run(update.Message.Text, update.Message.Chat.Id.ToString());
            }

            return Ok();
        }

        public async void SendMessage(long userId, string data)
        {
            var action = client.SendTextMessageAsync(chatId: userId, data);
            Message sentMessage = await action;
        }

        public void SendMessage(string userId, string data)
        {
            var chatId = long.Parse(userId);
            SendMessage(chatId, data);
        }
    }
}
