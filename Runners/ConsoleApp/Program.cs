using BasePlugin;
using BasePlugin.Interfaces;
using BasePlugin.Records;
using CountDown;
using Dals;
using DiceRoller;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace ConsoleApp
{
    class Program : IMessageSender
    {
        static void Main()
        {
            Program program = new Program();
            program.Main(program);
        }

        void Main(IMessageSender messageSender)
        {
            var pluginExecutor = new PluginExecutor(messageSender, new MemoryDal(), new PluginsMenu(), new PluginsManager());
            Console.WriteLine("Hi! I am Your amazing bot and I will be happy to be of service to you on several topics.");
            Console.WriteLine("Please type the number of the plugin you would like to help, or type help to get the list of options.");
            while (true)
            {
                var PluginId = pluginExecutor.GetCurrentUserPluginId(" ");
                PluginId = (PluginId != null && PluginId != "") ? "\\" + PluginId : "";
                Console.Write("main{0}> ", PluginId);
                var msg = Console.ReadLine();
                pluginExecutor.Run(msg, " ");
            }
        }

        public void SendMessage(string userId, string data)
        {
            Console.WriteLine(data);
        }
    }
}
