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
            Console.WriteLine(pluginExecutor.Run("HelP", ""));

            Console.WriteLine(pluginExecutor.Run("4", ""));
            Console.WriteLine(pluginExecutor.Run("test", ""));
            Console.WriteLine(pluginExecutor.Run("again", ""));
            Console.WriteLine(pluginExecutor.Run("and again...", ""));
            Console.WriteLine(pluginExecutor.Run("exit", ""));

            Console.WriteLine(pluginExecutor.Run("3", ""));
            Console.WriteLine(pluginExecutor.Run("3", ""));
            Console.WriteLine(pluginExecutor.Run("7", ""));
            Console.WriteLine(pluginExecutor.Run("ht", ""));

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }
    }
}
