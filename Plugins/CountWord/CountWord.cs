using BasePlugin;
using BasePlugin.Interfaces;
using BasePlugin.Records;
using System;
using System.Collections.Generic;

namespace CountWord
{
    public class CountWordPlugin : IPlugin
    {
        public static string _Id = "countword";
        public string Id => _Id;

        public PluginOutput Execute(PluginInput input)
        {
            var wordCount = new int();
            var index = new int();
            wordCount = 0;
            index = 0;

            var str = input.Message.Trim();

            if (string.IsNullOrWhiteSpace(str))
            {
                return new PluginOutput("CountWord. Enter any text after the digit 6 to Count the number of words in the text");
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
            return new PluginOutput($"The number of wrods in the text is: {wordCount} \r\nEnter new any text after the digit 6 to Count the number of words in the text.");
        }
    }
}
