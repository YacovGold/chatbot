using BasePlugin;
using BasePlugin.Interfaces;
using BasePlugin.Records;
using System;
using System.Collections.Generic;

namespace CountDown
{
    public class CountDownPlugin : IPluginWithScheduler
    {
        IScheduler _scheduler;

        public CountDownPlugin(IScheduler scheduler) => _scheduler = scheduler;

        public const string _Id = "count-down";
        public string Id => _Id;

        public void Execute(PluginInput input)
        {
            if (input.Message == "")
            {
                _scheduler.Schedule(TimeSpan.FromSeconds(1), Id, "", input.Callbacks);
                input.Callbacks.SendMessage(Resources.Plugins.Countdown_Started);
            }
            else
            {
                try
                {
                    var interval = int.Parse(input.Message);
                    _scheduler.Schedule(TimeSpan.FromSeconds(interval), Id, "", input.Callbacks);
                    input.Callbacks.SendMessage(Resources.Plugins.Countdown_Started);
                }
                catch (FormatException)
                {
                    input.Callbacks.SendMessage(Resources.Plugins.Countdown_Failed);
                }
            }
        }

        public void OnScheduler(ICallbacks callbacks, string data)
        {
            callbacks.SendMessage(Resources.Plugins.Countdown_Fired);
        }
    }
}
