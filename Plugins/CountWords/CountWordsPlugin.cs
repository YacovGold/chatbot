using System;
using System.Linq;
using System.Text.Json;
using BasePlugin;
using BasePlugin.Interfaces;
using BasePlugin.Records;


namespace CountWords
{
    record Session(int NumberOfWords);
    public class CountWordsPlugin : IPlugin
    {
        public string Id => throw new NotImplementedException();


        public bool CanExecute(string input, string session)
        {
            return true;
        }

        public PluginOutput Execute(PluginInput input)
        {
            int numberOfWords = input.Message.Split(' ').ToList().Where(s => !string.IsNullOrEmpty(s)).Count();
            var ses = new Session(numberOfWords);
            return new PluginOutput("The number of words is " + numberOfWords, JsonSerializer.Serialize(ses));
        }
    }
}
