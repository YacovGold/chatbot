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
using Timers;
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
        public IPlugin CreatePlugin(string id) => id switch
        {
            CountDownPlugin._Id => new CountDownPlugin(new Scheduler(this)),
            DiceRollerPlugin._Id => new DiceRollerPlugin(),
            CounterPlugin._Id => new CounterPlugin(),
            EchoPlugin._Id => new EchoPlugin(),
            ListPlugin._Id => new ListPlugin(),
            CountTheWordsPlugin._Id => new CountTheWordsPlugin(),
            CountWordPlugin._Id => new CountWordPlugin(),
            AlarmClockPlugin._Id => new AlarmClockPlugin(new Scheduler(this)),
            TimersPlugin._Id => new TimersPlugin(new Scheduler(this)),
            Date._Id => new Date(),
            NumerologyCalculatorPlugin._Id => new NumerologyCalculatorPlugin(),
            IsPrimeNumberPlugin._Id => new IsPrimeNumberPlugin(),
            FactorialPlugin._Id => new FactorialPlugin(),
            QuadraticEquationPlugin._Id => new QuadraticEquationPlugin(),
            CalculatorPlugin._Id => new CalculatorPlugin(),
            DateConversionToJewishPlugin._Id => new DateConversionToJewishPlugin(),
            TriviaPlugin._Id => new TriviaPlugin(),

            _ => throw new NotImplementedException(),
        };

        static public readonly IReadOnlyList<string> plugins = new List<string>
        {
            DiceRollerPlugin._Id,
            CountDownPlugin._Id,
            CounterPlugin._Id,
            EchoPlugin._Id,
            ListPlugin._Id,
            CountTheWordsPlugin._Id,
            CountWordPlugin._Id,
            TimersPlugin._Id,
            AlarmClockPlugin._Id,
            Date._Id,
            NumerologyCalculatorPlugin._Id,
            IsPrimeNumberPlugin._Id,
            FactorialPlugin._Id,
            QuadraticEquationPlugin._Id,
            DateConversionToJewishPlugin._Id,
            CalculatorPlugin._Id,
            TriviaPlugin._Id,
        };
    }
}
