using System;

namespace BasePlugin.Interfaces
{
    public interface ICallbacks
    {
        Action StartSession { get; }
        Action EndSession { get; }
        Action<string> SendMessage { get; }
        Action<string> SavePluginUserData { get; }
    }
}
