using BasePlugin.Interfaces;
using BasePlugin.Records;

namespace IsPrimeNumber
{
    public class IsPrimeNumberPlugin : IPlugin
    {
        public const string _Id = "Is Prime Number";
        public string Id => _Id;
    
    public void Execute(PluginInput input)
        {
            
            bool divider;

            if(input.Message==string.Empty)
            {
                input.Callbacks.StartSession();
                input.Callbacks.SendMessage(Resources.Plugins.IsPrimeNumber_Welcome);
            }
            else if (input.Message.ToLower() == "exit")
            {
                input.Callbacks.EndSession();
                input.Callbacks.SendMessage(Resources.Plugins.IsPrimeNumber_Stopped);
            }
            else if(int.TryParse(input.Message,out int num1))
            {
                for (int i = 2; i < Math.Sqrt(num1); i++)
                {
                    divider = num1 % i == 0;

                    if (divider)
                        input.Callbacks.SendMessage(string.Format(Resources.Plugins.IsPrimeNumber_NotPrime, num1, i));
                    else
                        input.Callbacks.SendMessage(string.Format(Resources.Plugins.IsPrimeNumber_IsPrime, num1));
                }
            }
            else
            {
                input.Callbacks.SendMessage(Resources.Plugins.IsPrimeNumber_TryAgin);
            }
        }
    }
}
