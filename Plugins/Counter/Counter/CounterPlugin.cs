using BasePlugin.Interfaces;
using BasePlugin.Records;
using System;

namespace Counter
{
    public class CounterPlugin : IPlugin
    {
        public static string _Id => "counter";
        public string Id => _Id;

        public bool CanExecute(string input, string session) => true;

        public PluginOutput Execute(string input, string session, ICallbacks callbacks)
        {
            var lastCount = session == null ? 0 : int.Parse(session);
            var result = (lastCount + 1).ToString();
            return new PluginOutput(result, result);
        }
    }
}
