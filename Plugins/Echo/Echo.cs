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

        public bool CanExecute(string input, string session) => true;

        public PluginOutput Execute(string input, string session, ICallbacks callbacks)
        {
            if (input == "")
            {
                callbacks.StartSession();
                return new PluginOutput("Echo started. Enter 'Exit' to stop.", null);
            }
            else if (input.ToLower() == "exit")
            {
                callbacks.StartSession();
                return new PluginOutput("Echo stopped.", null);
            }
            else
            {
                return new PluginOutput(input.ToUpper(), null);
            }
        }
    }
}
