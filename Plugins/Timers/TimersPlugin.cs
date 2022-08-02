using BasePlugin;
using BasePlugin.Interfaces;
using BasePlugin.Records;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Timers
{
    public class TimersPlugin : IPluginWithScheduler
    {
        IScheduler _scheduler;

        public TimersPlugin(IScheduler scheduler) => _scheduler = scheduler;

        public static string _Id = "Timers";
        public string Id => _Id;

        public void Execute(PluginInput input)
        {
            if (input.Message == "")
            {
                _scheduler.Schedule(TimeSpan.FromSeconds(1), Id, "", input.Callbacks);
                input.Callbacks.SendMessage("Countdown started.");
                return;
            }
            else
            {
                try
                {
                    var dateTime = DateTime.Now;
                    var hour = int.Parse(input.Message.Split(':').First());
                    var Minute = int.Parse(input.Message.Split(':').Skip(1).First());


                    var h = dateTime.Hour * 60 + dateTime.Minute;
                    var m = hour * 60 + Minute;
                    var sum = m - h;
                    var one = sum * 60 - dateTime.Second;

                    _scheduler.Schedule(TimeSpan.FromSeconds(one), Id, "", input.Callbacks);

                    input.Callbacks.SendMessage("Countdown started.");
                    return;
                }
                catch (FormatException)
                {
                    input.Callbacks.SendMessage("Countdown failed, string input nust represent vaild seconds.");
                    return;
                }
            }
        }

        public void OnScheduler(ICallbacks callbacks, string data)
        {
            callbacks.SendMessage("Fired.");
        }
    }
}
