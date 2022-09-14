using Dals;
using Infrastructure;
using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using BasePlugin.Interfaces;
using Services;

namespace Telegram.Bot.Examples.Polling
{
    public class Handlers
    {

        private readonly IServiceProvider _serviceProvider;
        public Handlers(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
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
            var pluginExecutor = (PluginExecutor)_serviceProvider.GetService(typeof(PluginExecutor));
            Console.WriteLine($"Receive message type: {message.Type}");
            if (message.Type != MessageType.Text)
                return Task.CompletedTask;
            BasePlugin.Records.User user = new BasePlugin.Records.User(message.Chat.Id.ToString(), RunnerType.Telegram);
            pluginExecutor.Run(message.Text, user);
            return Task.CompletedTask;
        }
    }
}
