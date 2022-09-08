using BasePlugin.Interfaces;
using CountDown;
using Counter;
using CountTheWords;
using DiceRoller;
using Echo;
using CountWord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlarmClock;
using List;
using DataTimeNow;
using NumerologyCalculator;
using IsPrimeNumber;
using Factorial;
using QuadraticEquation;
using DateConversionToJewish;
using Calculator;
using Trivia;

namespace Infrastructure
{
    public class PluginsManager
    {
        private readonly IEnumerable<IPlugin> _plugins;

        public PluginsManager(IEnumerable<IPlugin> plugins) => _plugins = plugins;

        public IPlugin GetPlugin(string id)
        {
            var plugin = _plugins.Single(p => p.Id == id);
            return plugin;
        }

        public List<string> GetPlugins()
        {
            return _plugins.Select(v => v.Id).ToList();
        }
    }
}
