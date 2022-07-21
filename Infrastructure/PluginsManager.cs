using BasePlugin.Interfaces;
using CountDown;
using Counter;
using CountTheWords;
using DiceRoller;
using Echo;
using ListPlugin;
using CountWord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlarmClock;

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
            else if (id == ListPlugin.ListPlugin._Id)
            {
                return new ListPlugin.ListPlugin();
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
            ListPlugin.ListPlugin._Id,
            CountTheWordsPlugin._Id,
            CountWordPlugin._Id,
            AlarmClockPlugin._Id
        };
    }
}
