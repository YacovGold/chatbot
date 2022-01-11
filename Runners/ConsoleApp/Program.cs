using BasePlugin;
using BasePlugin.Interfaces;
using BasePlugin.Records;
using CountDown;
using Dals;
using DiceRoller;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var pluginExecutor = new PluginExecutor(new MemoryDal(), new PluginsMenu(), new PluginsManager());

            while (true)
            {
                var msg = Console.ReadLine();
                var res = pluginExecutor.Run(msg, "");
                Console.WriteLine(res);
            }
        }
    }
}
