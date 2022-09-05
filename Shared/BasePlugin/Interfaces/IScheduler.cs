using System;

namespace BasePlugin.Interfaces
{
    public interface IScheduler
    {
        void Schedule(TimeSpan ts, string pluginId, string data, ICallbacks callbacks);
        void Schedule(DateTime dt, string pluginId, string data, ICallbacks callbacks);
    }
}
