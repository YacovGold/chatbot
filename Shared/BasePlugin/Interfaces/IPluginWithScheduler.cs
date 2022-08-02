namespace BasePlugin.Interfaces
{
    public interface IPluginWithScheduler : IPlugin
    {
        void OnScheduler(ICallbacks callbacks, string data);
    }
}
