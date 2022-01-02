using BasePlugin.Records;
using System.Collections.Generic;

namespace BasePlugin.Interfaces
{

    public interface IPlugin
    {
        public string Id { get; }
        public PluginOutput Execute(PluginInput input);
    }
}
