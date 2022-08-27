using System;
using System.Threading.Tasks;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using System.Threading;
using Telegram.Bot.Extensions.Polling;

namespace Telegram.Bot.Examples.Polling
{
    public static class Program
    {
        public static async Task Main()
        {
            var value = Environment.GetEnvironmentVariable("TelegramKey");
            var Bot = new TelegramBotClient(value);
            User me = await Bot.GetMeAsync();
            Console.Title = me.Username ?? "My awesome Bot";
            var handlers = new Handlers(Bot);
            ReceiverOptions receiverOptions = new() { AllowedUpdates = { } };
            Bot.StartReceiving(handlers.HandleUpdateAsync, handlers.HandleErrorAsync);
            Console.WriteLine($"Start listening for @{me.Username}");
            Console.ReadLine();
        }
    }
}