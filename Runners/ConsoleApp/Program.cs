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
        private string level = "";
        static void Main()
        {
            Program program = new Program();
            program.Main(program);
        }

        void Main(IMessageSender messageSender)
        {
            var pluginExecutor = new PluginExecutor(messageSender, new MemoryDal(), new PluginsMenu(), new PluginsManager());
            Console.WriteLine("Hi! I am Your amazing bot and I will be happy to be of service to you on several topics.");
            Console.WriteLine("Please type the number of the plugin you would like to help, or type help to get the list of options.\r\nAt any point you can type MAIN or HOME to return to the main menu.");
            while (true)
            {
                Console.Write("main{0}> ", level);
                var msg = Console.ReadLine();
                level=pluginExecutor.Run(msg, " ");
               
            }
        }

        public void SendMessage(string userId, string data)
        {
            Console.WriteLine(data);
        }
    }
}
