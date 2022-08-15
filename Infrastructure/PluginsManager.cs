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


namespace Infrastructure
{
    public class PluginsManager
    {
        public IPlugin CreatePlugin(string id)
        {
            if (id == CountDownPlugin._Id)
            {
                return new CountDownPlugin(new Scheduler(this));
            }
            else if (id == DiceRollerPlugin._Id)
            {
                return new DiceRollerPlugin();
            }
            else if (id == CounterPlugin._Id)
            {
                return new CounterPlugin();
            }
            else if (id == EchoPlugin._Id)
            {
                return new EchoPlugin();
            }
            else if (id == ListPlugin._Id)
            {
                return new ListPlugin();
            }
            else if (id == CountTheWordsPlugin._Id)
            {
                return new CountTheWordsPlugin();
            }
            else if (id == CountWordPlugin._Id)
            {
                return new CountWordPlugin();
            }
            else if (id == AlarmClockPlugin._Id)
            {
                return new AlarmClockPlugin(new Scheduler(this));
            }
            else if (id == TimersPlugin._Id)
            {
                return new TimersPlugin(new Scheduler(this));
            }
            else if (id == Date._Id)
            {
                return new Date();
            }
            else if (id == NumerologyCalculatorPlugin._Id)
            {
                return new NumerologyCalculatorPlugin();
            }

           else if (id == IsPrimeNumberPlugin._Id)
            {
                return new IsPrimeNumberPlugin();

            else if (id == FactorialPlugin._Id)
            {
                return new FactorialPlugin();
            }
            else if (id == QuadraticEquationPlugin._Id)
            {
                return new QuadraticEquationPlugin();

            }
            else
            {
                throw new NotImplementedException();
            }
        }

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
            IsPrimeNumberPlugin._Id
            FactorialPlugin._Id,
            QuadraticEquationPlugin._Id

        };
    }
}
