using BasePlugin.Interfaces;
using BasePlugin.Records;

namespace Factorial
{
    public class FactorialPlugin : IPlugin
    {
        public static string _Id => "factorial";

        public string Id => _Id;

        public void Execute(PluginInput input)
        {
            if (input.Message == "")
            {
                input.Callbacks.StartSession();
                input.Callbacks.SendMessage("insert the number to calculate its factorial");
            }
            else if (input.Message.ToLower() == "exit")
            {
                input.Callbacks.EndSession();
                input.Callbacks.SendMessage("calculat factorial stopeed..");
            }
            else
            {
                var factorial = input.Message;
                var result = 1.0;
                var factorialNumber = int.Parse(factorial);

                for (int i = 1; i <= factorialNumber; i++)
                {
                    result *= i;
                }
                input.Callbacks.SendMessage($"The factorial is: {result}");
            }
        }
    }
}