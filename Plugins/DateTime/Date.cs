using BasePlugin.Interfaces;
using BasePlugin.Records;
using System;

namespace DataTimeNow
{
    public class Date : IPlugin
    {
        public static string _Id => "Date Time Now";

        public string Id => _Id;

        public PluginOutput Execute(PluginInput input)
        {
            var res = new PluginOutput("The Date is: " + DateTime.Now.ToString());
            return res;
        }
    }
}
