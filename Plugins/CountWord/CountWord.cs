using BasePlugin;
using BasePlugin.Interfaces;
using BasePlugin.Records;
using System;
using System.Collections.Generic;

namespace CountWord
{
    public class CountWordPlugin : IPlugin
    {
        public const string _Id = "countword";
        public string Id => _Id;

        public void Execute(PluginInput input)
        {
            var wordCount = new int();
            var index = new int();
            wordCount = 0;
            index = 0;

            var str = input.Message.Trim();

            if (string.IsNullOrWhiteSpace(str))
            {
                input.Callbacks.SendMessage("CountWord. Enter any text after the digit 6 to Count the number of words in the text");
                return;
            }
            while (index < str.Length && char.IsWhiteSpace(str[index]))
                index++;
            while (index < str.Length)
            {
                while (index < str.Length && !char.IsWhiteSpace(str[index]))
                    index++;
                wordCount++;
                while (index < str.Length && char.IsWhiteSpace(str[index]))
                    index++;
            }
            input.Callbacks.SendMessage($"The number of wrods in the text is: {wordCount} \r\nEnter new any text after the digit 6 to Count the number of words in the text.");
        }
    }
}
