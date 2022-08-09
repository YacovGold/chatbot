using BasePlugin.Interfaces;
using BasePlugin.Records;

namespace NumerologyCalculator
{
    public class NumerologyCalculatorPlugin : IPlugin
    {
        public const string _Id = "numerology";

        public string Id => _Id;

        public void Execute(PluginInput input)
        {
            if (input.Message == "")
            {
                input.Callbacks.StartSession();
                input.Callbacks.SendMessage("please type a letter to know the numerology..");
            }
            else if (input.Message.ToLower() == "exit")
            {
                input.Callbacks.StartSession();
                input.Callbacks.SendMessage("calculat numerology stopeed..");
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
                input.Callbacks.SendMessage($"The value of  {letterUser} is: {sum}");
            }
        }
    }
}