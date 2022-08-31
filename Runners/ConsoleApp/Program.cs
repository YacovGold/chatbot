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
        private string level = null;
        static void Main()
        {
            Program program = new Program();
            program.Main(program);
        }

        void Main(IMessageSender messageSender)
        {
            var pluginExecutor = new PluginExecutor(messageSender, new MemoryDal(), new PluginsMenu(), new PluginsManager());
            var txt1 = "Hi! I am Your amazing bot and I will be happy to be of service to you on several topics.";
            var txt2 = "Please type the number of the plugin you would like to help, or type help to get the list of options.";
            var txt3 = "At any point you can type MAIN or HOME to return to the main menu.";
            OutputTextWithDelay(txt1);
            Thread.Sleep(1000);
            OutputTextWithDelay(txt2);
            Thread.Sleep(1000);
            OutputTextWithDelay(txt3);
            while (true)
            {
                level = (level != null) ? "\\" + level : "";
                Console.Write("main{0}> ", level);
                var msg = Console.ReadLine();
                level = pluginExecutor.Run(msg, " ");
            }
        }

        private void OutputTextWithDelay(string txt)
        {
            for (int i = 0; i < txt.Length; i++)
            {
                Console.Write(txt[i]);
                Thread.Sleep(15);
            }
            Console.WriteLine();
        }

        public void SendMessage(string userId, string data)
        {
            Console.WriteLine(data);
        }
    }
}
