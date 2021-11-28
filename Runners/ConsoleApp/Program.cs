using BasePlugin;
using BasePlugin.Interfaces;
using BasePlugin.Records;
using CountDown;
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
            var plugin = new PluginsManager().CreatePlugin(CountDownPlugin._Id);
            plugin.Execute("", null, null);
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }
    }
}
