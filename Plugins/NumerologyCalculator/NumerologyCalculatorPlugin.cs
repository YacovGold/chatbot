using BasePlugin.Interfaces;
using BasePlugin.Records;

namespace NumerologyCalculator
{
    public class NumerologyCalculatorPlugin : IPlugin
    {
        public static string _Id => "numerology";

        public string Id => _Id;

        public PluginOutput Execute(PluginInput input)
        {
            if (input.Message == "")
            {
                input.Callbacks.StartSession();
                return new PluginOutput("please type a letter to know the numerology..");
            }
            else if (input.Message.ToLower() == "exit")
            {
                input.Callbacks.StartSession();
                return new PluginOutput("calculat numerology stopeed..");
            }
            else
            {
                var sum = 0;
                var letterUser = input.Message;

                for (var i = 0; i < letterUser.Length; i++)
                {
                    if (letterUser[i] == ' ')
                    {
                        continue;
                    }
                    sum += letterUser[i] - 96;
                }

                return new PluginOutput($"The value of  {letterUser} is: {sum}");
            }
        }
    }
}