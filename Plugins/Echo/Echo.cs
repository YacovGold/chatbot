using BasePlugin;
using BasePlugin.Interfaces;
using BasePlugin.Records;
using System;
using System.Collections.Generic;

namespace Echo
{
    public class EchoPlugin : IPlugin
    {
        public static string _Id = "echo";
        public string Id => _Id;

        public PluginOutput Execute(PluginInput input)
        {
            if (input.Message == "")
            {
                input.Callbacks.StartSession();
                return new PluginOutput("Echo started. Enter 'Exit' to stop.");
            }
            else if (input.Message.ToLower() == "exit")
            {
                input.Callbacks.EndSession();
                return new PluginOutput("Echo stopped.");
            }
            else
            {
                return new PluginOutput(input.Message.ToUpper());
            }
        }
    }
}
