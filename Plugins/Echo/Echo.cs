using BasePlugin;
using BasePlugin.Interfaces;
using BasePlugin.Records;
using System;
using System.Collections.Generic;

namespace Echo
{
    public class EchoPlugin : IPlugin
    {
        public const string _Id = "echo";
        public string Id => _Id;

        public void Execute(PluginInput input)
        {
            if (input.Message == "")
            {
                input.Callbacks.StartSession();
                input.Callbacks.SendMessage("Echo started. Enter 'Exit' to stop.");
            }
            else if (input.Message.ToLower() == "exit")
            {
                input.Callbacks.EndSession();
                input.Callbacks.SendMessage("Echo stopped.");
            }
            else
            {
                input.Callbacks.SendMessage(input.Message.ToUpper());
            }
        }
    }
}
