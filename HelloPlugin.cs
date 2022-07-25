using BasePlugin.Interfaces;
using BasePlugin.Records;
using System;

namespace Hello
{
    public class HelloPlugin : IPlugin
    {
        public static string _Id = "hello";
        public string Id => _Id;
        public PluginOutput Execute(PluginInput input)
        {
            var result = new PluginOutput("Hello");
            return result;
        }
    }
}

   