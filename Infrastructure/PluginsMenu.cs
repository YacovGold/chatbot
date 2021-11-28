using BasePlugin.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class PluginsMenu
    {
        public string PlaginsHelp()
        {
            var lines = PluginsManager.plugins.Select((p, i) => i + 1 + ". " + p);
            var result = string.Join("\r\n", lines);
            return result;
        }

    }
}
