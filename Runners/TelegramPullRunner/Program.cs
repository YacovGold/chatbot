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
            using var cts = new CancellationTokenSource();
            ReceiverOptions receiverOptions = new() { AllowedUpdates = { } };
            Bot.StartReceiving(Handlers.HandleUpdateAsync, Handlers.HandleErrorAsync);
            Console.WriteLine($"Start listening for @{me.Username}");
            Console.ReadLine();
            cts.Cancel();
        }


        private static async Task BotOnMessageReceived(ITelegramBotClient botClient, Message message)
        {

            Console.WriteLine($"Receive message type: {message.Type}");
            if (message.Type != MessageType.Text)
                return;
            var action = botClient.SendTextMessageAsync(chatId: message.Chat.Id, "you asked me for:" + message.Text);
            Message sentMessage = await action;
        }
    }
}