using BasePlugin.Interfaces;
using BasePlugin.Records;

namespace IsPrimeNumber
{
    public class IsPrimeNumberPlugin : IPlugin
    {
        public static string _Id => "Is Prime Number";
        public string Id => _Id;
    
    public void Execute(PluginInput input)
        {
            
            bool divider;

            if(input.Message==string.Empty)
            {
                input.Callbacks.StartSession();
                input.Callbacks.SendMessage("Please type a number: ");
            }
            else if (input.Message.ToLower() == "exit")
            {
                input.Callbacks.EndSession();
                input.Callbacks.SendMessage("we are sorry that you leave");
            }
            else if(int.TryParse(input.Message,out int num1))
            {
                for (int i = 2; i < Math.Sqrt(num1); i++)
                {
                    divider = num1 % i == 0;

                    if (divider)
                        input.Callbacks.SendMessage(num1 + " is not a prime number, because is divide in " + i);

                }

            }
            else
            {
                input.Callbacks.SendMessage("try again, enter a number");
            }
        }
    }
}