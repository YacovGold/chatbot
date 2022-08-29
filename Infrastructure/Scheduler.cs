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

        public void Schedule(DateTime dt, string pluginId, string data, ICallbacks callbacks)
        {
            var userTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "Israel Standard Time");

            var ts = new TimeSpan(dt.Hour, dt.Minute, userTime.Second);
            var tsNew = new TimeSpan(userTime.Hour, userTime.Minute, userTime.Second);
            var tsNew2 = ts - tsNew;
            var interval = tsNew2.TotalSeconds - userTime.Second;
            Schedule(TimeSpan.FromSeconds(interval), pluginId, "", callbacks);
        }

        private async Task _Schedule(TimeSpan ts, string pluginId, string data, ICallbacks callbacks)
        {
            await Task.Delay(ts);
            var plugin = (IPluginWithScheduler)_pluginsManager.CreatePlugin(pluginId);
            plugin.OnScheduler(callbacks, data);
        }
    
    }
}
