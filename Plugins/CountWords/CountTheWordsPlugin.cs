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
                input.Callbacks.SendMessage(Resources.Plugins.CountWords_Welcome);
            }
            else if (input.Message.ToLower() == "exit")
            {
                input.Callbacks.EndSession();
                input.Callbacks.SendMessage(Resources.Plugins.CountWords_Stopped);
            }
            else
            {
                var numberOfWords = input.Message.Split(' ').Where(s => !string.IsNullOrEmpty(s)).Count();

                input.Callbacks.SavePluginUserData(numberOfWords.ToString());
                input.Callbacks.SendMessage(string.Format(Resources.Plugins.CountWords_Num,numberOfWords));
            }
        }
    }
}
