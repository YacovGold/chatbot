using BasePlugin.Records;
using System.Collections.Generic;

namespace BasePlugin.Interfaces
{

    public interface IPlugin
    {
        public string Id { get; }
        public bool CanExecute(string input, string session);
        public PluginOutput Execute(string input, string session, ICallbacks callbacks);
    }
}
