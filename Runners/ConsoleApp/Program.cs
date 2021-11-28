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
            var host = new Host(new MemoryDal());
            Console.WriteLine(host.Run("3", ""));
            Console.WriteLine(host.Run("3", ""));
            Console.WriteLine(host.Run("7", ""));

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }
    }
}
