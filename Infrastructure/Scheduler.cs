using BasePlugin.Interfaces;
using BasePlugin.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class Scheduler : IScheduler
    {
        PluginsManager _pluginsManager;

        public Scheduler(PluginsManager pluginsManager) => _pluginsManager = pluginsManager;

        public void Schedule(TimeSpan ts, string pluginId, string data)
        {
            _ = _Schedule(ts, pluginId, data);
        }

        private async Task _Schedule(TimeSpan ts, string pluginId, string data)
        {
            await Task.Delay(ts);
            var plugin = (IPluginWithScheduler)_pluginsManager.CreatePlugin(pluginId);
            plugin.OnScheduler(data);
        }
    }
}
