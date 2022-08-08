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
            var pluginExecutor = new PluginExecutor(messageSender, new DbDal(), new PluginsMenu(), new PluginsManager());

            while (true)
            {
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
