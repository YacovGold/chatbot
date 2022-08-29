using Dals;
using Infrastructure;
using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using BasePlugin.Interfaces;
namespace Telegram.Bot.Examples.Polling
{
    public class Handlers : IMessageSender
    {
        private static PluginExecutor pluginExecutor;
        private ITelegramBotClient _botClient;

        public Handlers(ITelegramBotClient botClient)
        {
            pluginExecutor ??= new PluginExecutor(this, new DbDal(), new PluginsMenu(), new PluginsManager());
            _botClient = botClient;
        }

        public Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }

        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var handler = BotOnMessageReceived(botClient, update.Message!);
            try
            {
                await handler;
            }
            catch (Exception exception)
            {
                await HandleErrorAsync(botClient, exception, cancellationToken);
            }
        }

        private Task BotOnMessageReceived(ITelegramBotClient botClient, Message message)
        {
            Console.WriteLine($"Receive message type: {message.Type}");
            if (message.Type != MessageType.Text)
                return Task.CompletedTask;
            pluginExecutor.Run(message.Text, message.Chat.Id.ToString());
            return Task.CompletedTask;
        }

        public async void SendMessage(long userId, string data)
        {
            var action = _botClient.SendTextMessageAsync(chatId: userId, data);
            Message sentMessage = await action;
        }

        public void SendMessage(string userId, string data)
        {
            var chatId = long.Parse(userId);
            SendMessage(chatId, data);
        }
    }
}
