using BasePlugin.Interfaces;
using BasePlugin.Records;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class Scheduler : IScheduler
    {
        PluginsManager _pluginsManager;

        public Scheduler(PluginsManager pluginsManager) => _pluginsManager = pluginsManager;

        public void Schedule(TimeSpan ts, string pluginId, string data, ICallbacks callbacks)
        {
            _ = _Schedule(ts, pluginId, data, callbacks);

        }

        private async Task _Schedule(TimeSpan ts, string pluginId, string data, ICallbacks callbacks)
        {
            await Task.Delay(ts);
            var plugin = (IPluginWithScheduler)_pluginsManager.CreatePlugin(pluginId);
            plugin.OnScheduler(callbacks, data);
        }
    }
}
