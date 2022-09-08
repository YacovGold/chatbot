using BasePlugin;
using BasePlugin.Interfaces;
using BasePlugin.Records;
using BasePlugin.Services;
using CountDown;
using Dals;
using DiceRoller;
using Infrastructure;
using Microsoft.Extensions.Hosting;
using System;

var builder = Host.CreateDefaultBuilder();

builder.ConfigureServices(services => services.RegisterServices());

var host = builder.Build();

var pluginExecutor = (PluginExecutor)host.Services.GetService(typeof(PluginExecutor));

Console.WriteLine("Hi! I am Your amazing bot and I will be happy to be of service to you on several topics.");
Console.WriteLine("Please type the number of the plugin you would like to help, or type help to get the list of options.");
while (true)
{
    User user = new User(" ", RunnerType.ConsoleApp);
    var PluginId = pluginExecutor.GetCurrentUserPluginId(user);
    PluginId = (PluginId != null && PluginId != "") ? "\\" + PluginId : "";
    Console.Write("main{0}> ", PluginId);
    var msg = Console.ReadLine();
    pluginExecutor.Run(msg, user);
}


