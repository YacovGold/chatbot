using System;
using System.Threading.Tasks;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using System.Threading;
using Infrastructure;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot;
using Telegram.Bot.Examples.Polling;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateDefaultBuilder();
builder.ConfigureServices(services =>
{
    services.RegisterServices();
});

var host = builder.Build();

var value = Environment.GetEnvironmentVariable("TelegramKey");
var Bot = new TelegramBotClient(value);

User me = await Bot.GetMeAsync();
Console.Title = me.Username ?? "My awesome Bot";
ReceiverOptions receiverOptions = new() { AllowedUpdates = { } };
Handlers.serviceProvider = host.Services;
Bot.StartReceiving(Handlers.HandleUpdateAsync, Handlers.HandleErrorAsync);
Console.WriteLine($"Start listening for @{me.Username}");
Console.ReadLine();