using BasePlugin.Interfaces;
using BasePlugin.Records;

namespace Factorial
{
    public class FactorialPlugin : IPlugin
    {
        public const string _Id = "factorial";

        public string Id => _Id;

        public void Execute(PluginInput input)
        {
            if (input.Message == "")
            {
                input.Callbacks.StartSession();
                input.Callbacks.SendMessage(Resources.Plugins.Factorial_Welcome);
            }
            else if (input.Message.ToLower() == "exit")
            {
                input.Callbacks.EndSession();
                input.Callbacks.SendMessage(Resources.Plugins.Factorial_Stopped);
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
                input.Callbacks.SendMessage(string.Format(Resources.Plugins.Factorial_Res ,result));
            }
        }
    }
}