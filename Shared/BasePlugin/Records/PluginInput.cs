using BasePlugin.Interfaces;

namespace BasePlugin.Records
{
    public record PluginInput(string Message, string PersistentData, ICallbacks Callbacks);
}
