using BasePlugin.Interfaces;
using BasePlugin.Records;
using System;

namespace DataTimeNow
{
    public class Date : IPlugin
    {
        public const string _Id = "Date Time Now";

        public string Id => _Id;

        public void Execute(PluginInput input)
        {
            var userTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "Israel Standard Time");
            input.Callbacks.SendMessage(string.Format(Resources.Plugins.DataTimeNow_Res, userTime.ToString()));
        }
    }
}
