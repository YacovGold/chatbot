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
        private readonly PluginsManager _pluginsManager;
        public PluginsMenu(PluginsManager pluginsManager) => _pluginsManager = pluginsManager;
        public string PluginsHelp()
        {
            var lines = _pluginsManager.GetPlugins().Select((p, i) => $"{i + 1}. {p}").ToList();
            lines.Add("Sample: 4 the letters will be in upper case");
            var result = string.Join("\r\n", lines);
            return result;
        }
    }
}
