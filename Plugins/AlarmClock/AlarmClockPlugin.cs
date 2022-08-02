using BasePlugin.Interfaces;
using BasePlugin.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlarmClock
{
    public class AlarmClockPlugin : IPluginWithScheduler
    {
        IScheduler _scheduler;
        public AlarmClockPlugin(IScheduler scheduler) => _scheduler = scheduler;

        public static string _Id = "Alarm-Clock";
        public string Id => _Id;

        public void Execute(PluginInput input)
        {
            DateTime dateTime = DateTime.Now;
            var sh = input.Message.Split(':').First();
            var sm = input.Message.Split(':').Skip(1).First();

            if (int.TryParse(sh, out int a) && int.TryParse(sm, out int b))
            {
                var ts = new TimeSpan(a, b, dateTime.Second);
                var tsNew = new TimeSpan(dateTime.Hour, dateTime.Minute, dateTime.Second);
                var tsNew2 = ts - tsNew;
                var interval = tsNew2.TotalSeconds - dateTime.Second;

                if (input.Message == "")
                {
                    _scheduler.Schedule(TimeSpan.FromSeconds(1), Id, "", input.Callbacks);
                    input.Callbacks.SendMessage("Alarm Clock set");
                    return;
                }
                else
                {
                    _scheduler.Schedule(TimeSpan.FromSeconds(interval), Id, "", input.Callbacks);
                    input.Callbacks.SendMessage("Alarm Clock set");
                    return;
                }
            }
            else
            {
                input.Callbacks.SendMessage("Please write the time in format - aa: mm");
                return;
            }
        }

        public void OnScheduler(ICallbacks callbacks, string data)
        {
            callbacks.SendMessage("Alarm Clock fired");
        }
    }
}

