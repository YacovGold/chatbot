using BasePlugin.Interfaces;
using BasePlugin.Records;

namespace QuadraticEquation
{
    public class QuadraticEquationPlugin : IPlugin
    {
        public const string _Id = "quadraticEquation";
        public string Id => _Id;


        public void Execute(PluginInput input)
        {
            if (input.Message == "")
            {
                input.Callbacks.StartSession();
                input.Callbacks.SendMessage("Calculate a quadratic equation, Please enter entries of, type ' ' btween an integer: A B C:");
            }
            else
            {
                double d, shoresh, x1, x2;

                var a = double.Parse(input.Message.Split(' ')[0]);
                var b = double.Parse(input.Message.Split(' ')[1]);
                var c = double.Parse(input.Message.Split(' ')[2]);
                d = Math.Pow(b, 2) - 4 * a * c;

                if (d < 0)
                {
                    input.Callbacks.EndSession();
                    input.Callbacks.SendMessage("The above equation doesn't have any solution!");
                }
                else
                {
                    input.Callbacks.StartSession();
                    shoresh = Math.Sqrt(d);

                    x1 = ((-1) * b + shoresh) / (2 * a);
                    input.Callbacks.EndSession();

                    if (d == 0)
                    {
                        input.Callbacks.SendMessage("the first solution is: " + x1);
                    }
                    x2 = ((-1) * b - shoresh) / (2 * a);
                    input.Callbacks.EndSession();
                    input.Callbacks.SendMessage($"the first solution is:{x1}, the second solution is: {x2}");
                }
            }
        }
    }
}