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

        public const string _Id = "Alarm-Clock";
        public string Id => _Id;

        public void Execute(PluginInput input)
        {
            var userTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "Israel Standard Time");
            var dt = userTime;
            if(string.IsNullOrWhiteSpace(input.Message))
            {
                input.Callbacks.SendMessage("Please write the time in format - aa: mm");
                return;
            }
            var sh = input.Message.Split(':').First();
            var sm = input.Message.Split(':').Skip(1).First();


            if(int.TryParse(sh, out int h) && int.TryParse(sm, out int m))
            {
                var d1 = new DateTime(dt.Year, dt.Month, dt.Day, h, m, 0);

                if (d1<userTime)
                {
                    d1= d1.AddDays(1);
                }
                else
                {
                    _scheduler.Schedule(d1, Id, "", input.Callbacks);
                    input.Callbacks.SendMessage("Alarm Clock set");
                }
            }
            else
            {
                input.Callbacks.SendMessage("Please write the time in format - aa: mm");
            }
        }

        public void OnScheduler(ICallbacks callbacks, string data)
        {
            callbacks.SendMessage("Alarm Clock fired");
        }
    }
}

