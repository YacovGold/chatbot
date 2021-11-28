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

        public static string _Id = "count-down";
        public string Id => _Id;

        public bool CanExecute(string input, string session) => true;

        public PluginOutput Execute(string input, string session, ICallbacks callbacks)
        {
            if (input == "")
            {
                _scheduler.Schedule(TimeSpan.FromSeconds(1), Id, "");
                return new PluginOutput("Countdown started.", null);
            }
            else
            {
                try
                {
                    var interval = int.Parse(input);
                    _scheduler.Schedule(TimeSpan.FromSeconds(interval), Id, "");
                    return new PluginOutput("Countdown started.", null);
                }
                catch (FormatException)
                {
                    return new PluginOutput("Countdown failed, string input nust represent vaild seconds.", null);
                }
            }
        }

        public void OnScheduler(string data)
        {
            Console.WriteLine("Fired.");
        }
    }
}
