using BasePlugin.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace Services
{
    public class TelegramService : IService
    {

        private readonly ITelegramBotClient client;

        public TelegramService()
        {
            var value = Environment.GetEnvironmentVariable("TelegramKey");
            client = new TelegramBotClient(value);
        }

        public async void SendMessage(string userId, string data)
        {
            var chatId = long.Parse(userId);
            await client.SendTextMessageAsync(chatId: chatId, data);
        }
    }
}
