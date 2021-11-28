namespace BasePlugin.Interfaces
{
    public interface IPluginWithScheduler : IPlugin
    {
        void OnScheduler(string data);
    }
}
