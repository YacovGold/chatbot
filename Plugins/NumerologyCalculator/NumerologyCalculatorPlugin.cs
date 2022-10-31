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
                input.Callbacks.SendMessage(Resources.Plugins.Numerology_Welcome);
            }
            else if (input.Message.ToLower() == "exit")
            {
                input.Callbacks.EndSession();
                input.Callbacks.SendMessage(Resources.Plugins.Numerology_Stopped);
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
                input.Callbacks.EndSession();
                input.Callbacks.SendMessage(string.Format(Resources.Plugins.Numerology_Value, letterUser, sum));
            }
        }
    }
}