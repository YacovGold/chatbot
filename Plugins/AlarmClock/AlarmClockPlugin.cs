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
        public PluginOutput Execute(PluginInput input)
        {
            DateTime dateTime = DateTime.Now;
            var sh = input.Message.Split(':').First();
            var sm = input.Message.Split(':').Skip(1).First();

            if (double.TryParse(sh, out double a) && double.TryParse(sm, out double b))
            {

                int h = int.Parse(sh);
                int m = int.Parse(sm);
                var ts = new TimeSpan(h, m, 0);
                var tsNew = new TimeSpan(dateTime.Hour, dateTime.Minute, 0);
                var tsNew2 = ts - tsNew;


                double interval = tsNew2.TotalMinutes;
                if (input.Message == "")
                {
                    _scheduler.Schedule(TimeSpan.FromSeconds(1), Id, "");
                    return new PluginOutput("Countdown started.");
                }
                else
                {
                    _scheduler.Schedule(TimeSpan.FromMinutes(interval), Id, "");
                    return new PluginOutput("Countdown started.");
                }

            }
            else
            {
                return new PluginOutput("Please write the time in format - aa: mm");
            }
        }

        public void OnScheduler(string data)
        {
            Console.Beep(3000, duration: 2000);

        }
    }
}

