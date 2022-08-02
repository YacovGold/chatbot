using BasePlugin.Interfaces;
using BasePlugin.Records;
using System;

namespace Counter
{
    public class CounterPlugin : IPlugin
    {
        public static string _Id => "counter";
        public string Id => _Id;

        public void Execute(PluginInput input)
        {
            var lastCount = input.PersistentData == null ? 0 : int.Parse(input.PersistentData);
            var result = (lastCount + 1).ToString();
            input.Callbacks.SavePluginUserData(result);
            input.Callbacks.SendMessage(result);
        }
    }
}
