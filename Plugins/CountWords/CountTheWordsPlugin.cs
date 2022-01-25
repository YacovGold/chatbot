using System;
using System.Linq;
using System.Text.Json;
using BasePlugin.Interfaces;
using BasePlugin.Records;



namespace CountTheWords
{
   
    public class CountTheWordsPlugin : IPlugin
    {

        public static string _Id = "count-words";
        public string Id => _Id;


        public PluginOutput Execute(PluginInput input)
        {
            if (input.Message == "")
            {
                input.Callbacks.StartSession();
                return new PluginOutput("Count words started. Enter 'Exit' to stop.");
            }
            else if (input.Message.ToLower() == "exit")
            {
                input.Callbacks.EndSession();
                return new PluginOutput("Count words stopped.");
            }
            else
            {
                var numberOfWords = input.Message.Split(' ').Where(s => !string.IsNullOrEmpty(s)).Count();

                return new PluginOutput("The number of words is " + numberOfWords, numberOfWords.ToString());
            }
        }
    }
}
