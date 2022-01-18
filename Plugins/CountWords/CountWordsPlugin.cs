using System;
using System.Linq;
using System.Text.Json;
using BasePlugin.Interfaces;
using BasePlugin.Records;



namespace CountWords
{
   
    public class CountWordsPlugin : IPlugin
    {
        public string Id => throw new NotImplementedException();


      

        public PluginOutput Execute(PluginInput input)
        {
            var numberOfWords = input.Message.Split(' ').Where(s => !string.IsNullOrEmpty(s)).Count();
            
            return new PluginOutput("The number of words is " + numberOfWords,numberOfWords.ToString());
        }
    }
}
