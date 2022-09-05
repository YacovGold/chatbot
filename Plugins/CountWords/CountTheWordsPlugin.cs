using System;
using System.Linq;
using System.Text.Json;
using BasePlugin.Interfaces;
using BasePlugin.Records;



namespace CountTheWords
{

    public class CountTheWordsPlugin : IPlugin
    {

        public const string _Id = "count-words";
        public string Id => _Id;


        public void Execute(PluginInput input)
        {
            if (input.Message == "")
            {
                input.Callbacks.StartSession();
                input.Callbacks.SendMessage("Count words started. Enter 'Exit' to stop.");
            }
            else if (input.Message.ToLower() == "exit")
            {
                input.Callbacks.EndSession();
                input.Callbacks.SendMessage("Count words stopped.");
            }
            else
            {
                var numberOfWords = input.Message.Split(' ').Where(s => !string.IsNullOrEmpty(s)).Count();

                input.Callbacks.SavePluginUserData(numberOfWords.ToString());
                input.Callbacks.SendMessage("The number of words is " + numberOfWords);
            }
        }
    }
}
