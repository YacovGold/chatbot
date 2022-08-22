using BasePlugin.Interfaces;
using BasePlugin.Records;
using System;

namespace Calculator
{
    public class CalculatorPlugin : IPlugin
    {
        public const string _Id = "calculator";
        public string Id => _Id;
        public void Execute(PluginInput input)
        {
            if (input.Message == "")
            {
                input.Callbacks.SendMessage("please type a letter to know the numerology..");
            }
            else
            {
                var x = CalculatCalculator(input.Message);
                var y = x.ToString();
                input.Callbacks.SendMessage(y);
            }
        }
        private double CalculatCalculator(string input)
        {
            var numbers1 = input.Split('-', '+', '*', '/');
            var result = 0d;
            var counter = 1;
            var j = 0;

            if (input.Contains("/0"))
            {
                if (input.IndexOf("/0") + 1 != '.')     // Checking if the input contains a fractional number 0
                {
                    throw new InvalidOperationException("Do not divide by zero");
                }
            }
            if (input[0] == '-')                   // Checking if the input starts with a minus        
            {
                result = double.Parse(numbers1[1]) * -1;
                j++;
                counter++;
            }
            else
            {
                result = double.Parse(numbers1[0]);
            }
            for (int i = j; i < input.Length; i++)
            {
                if (i < input.Length - 2)
                {
                    if (input[i] == '-' || input[i] == '+')
                    {
                        if (input[i] == '+')
                        {
                            i += 2;
                            if (input[i] == '*' || input[i] == '/')
                            {
                                switch (input[i])
                                {
                                    case '*':
                                        result += double.Parse(numbers1[counter]) * double.Parse(numbers1[++counter]);
                                        break;
                                    case '/':
                                        result += double.Parse(numbers1[counter]) / double.Parse(numbers1[++counter]);
                                        break;
                                }
                                i++;
                            }
                            else
                            {
                                i -= 2;
                            }
                        }
                        else if (input[i] == '-')
                        {
                            i += 2;
                            if (input[i] == '*' || input[i] == '/')
                            {
                                switch (input[i])
                                {
                                    case '*':
                                        result -= double.Parse(numbers1[counter]) * double.Parse(numbers1[++counter]);
                                        break;
                                    case '/':
                                        result -= double.Parse(numbers1[counter]) / double.Parse(numbers1[++counter]);
                                        break;
                                }
                                i++;
                            }
                            else
                            {
                                i -= 2;
                            }
                        }
                    }
                }
                if (input[i] == '-' || input[i] == '+' || input[i] == '/' || input[i] == '*')
                {
                    switch (input[i])
                    {
                        case '-':
                            result -= double.Parse(numbers1[counter]);
                            break;
                        case '+':
                            result += double.Parse(numbers1[counter]);
                            break;
                        case '*':
                            result *= double.Parse(numbers1[counter]);
                            break;
                        case '/':
                            result /= double.Parse(numbers1[counter]);
                            break;
                    }
                    counter++;
                }
            }
            return result;
        }
    }
}