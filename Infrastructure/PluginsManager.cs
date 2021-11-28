using BasePlugin.Interfaces;
using CountDown;
using Counter;
using DiceRoller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            else
            {
                throw new NotImplementedException();
            }
        }

        static public readonly IReadOnlyList<string> plugins = new List<string>
        {
            DiceRollerPlugin._Id,
            CountDownPlugin._Id,
            CounterPlugin._Id
        };
    }
}
