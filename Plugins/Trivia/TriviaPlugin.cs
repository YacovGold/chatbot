using System;
using System.Reflection;
using System.Text.Json;
using BasePlugin.Interfaces;
using BasePlugin.Records;

namespace Trivia
{
    record PersistentDataStructure(double score, double numQest, int trueResult, bool triviaBegun);

    public class TriviaPlugin : IPlugin
    {
        private double score = 0;
        private int[] nums = new int[2];
        private int result;
        private int[] allResult = new int[4];
        private int trueResult;
        private double numQest = 0;
        private bool triviaBegun;

        public const string _Id = "Trivia";

        public string Id => _Id;
        public void Execute(PluginInput input)
        {
            if (string.IsNullOrEmpty(input.PersistentData) == false)
            {
                var trivia = JsonSerializer.Deserialize<PersistentDataStructure>(input.PersistentData)!;
                score = trivia.score;
                numQest = trivia.numQest;
                trueResult = trivia.trueResult;
                triviaBegun = trivia.triviaBegun;
            }
            if (input.Message == "" && string.IsNullOrEmpty(input.PersistentData) == true)
            {
                input.Callbacks.StartSession();
                input.Callbacks.SavePluginUserData(input.PersistentData);
                input.Callbacks.SendMessage(Resources.Plugins.Trivia_Welcome);
            }
            else
            {
                if (input.Message.ToLower() == "exit")
                {
                    input.Callbacks.EndSession();
                    if (numQest > 0)
                    {
                        triviaBegun = false;
                        double grade = score / numQest * 100;
                        int grade1 = (int)grade;
                        string mess = String.Format(Resources.Plugins.Trivia_EndMessage, grade1);
                        input.Callbacks.SendMessage(mess);
                    }
                    input.Callbacks.SavePluginUserData(null);
                }
                else if (input.Message.ToLower() == "start" && !triviaBegun)
                {
                    triviaBegun = true;
                    var data = GetTrivia();
                    var currnetTrivia = new PersistentDataStructure(score, numQest, trueResult, triviaBegun);
                    input.Callbacks.SavePluginUserData(JsonSerializer.Serialize(currnetTrivia));
                    input.Callbacks.SendMessage(data);
                }
                else if (triviaBegun)
                {
                    if (input.Message == "" && string.IsNullOrEmpty(input.PersistentData) == false)
                    {
                        input.Callbacks.SavePluginUserData(input.PersistentData);
                    }
                    else if (input.Message.Length > 1 || input.Message[0] < '1' || input.Message[0] > '4')
                    {
                        input.Callbacks.SavePluginUserData(input.PersistentData);
                        input.Callbacks.SendMessage(Resources.Plugins.Trivia_ErrorOutScope);
                    }
                    else
                    {
                        numQest++;
                        if (int.Parse(input.Message) == trueResult + 1)
                        {
                            score++;
                            var data = GetTrivia();
                            var currnetTrivia = new PersistentDataStructure(score, numQest, trueResult, triviaBegun);
                            input.Callbacks.SavePluginUserData(JsonSerializer.Serialize(currnetTrivia));
                            input.Callbacks.SendMessage($"{Resources.Plugins.Trivia_Success}\n{data}");
                        }
                        else
                        {
                            var mess = string.Format(Resources.Plugins.Trivia_Wrong, trueResult + 1);
                            var data = GetTrivia();
                            var currnetTrivia = new PersistentDataStructure(score, numQest, trueResult, triviaBegun);
                            input.Callbacks.SavePluginUserData(JsonSerializer.Serialize(currnetTrivia));
                            input.Callbacks.SendMessage($"{mess}\n{data} ");
                        }
                    }
                }
                else
                {
                    input.Callbacks.SendMessage(Resources.Plugins.Trivia_ErrorIncorrectTyping);

                }
            }
        }

        public string GetTrivia()
        {
            nums = RndNums(2, 10, true);
            result = nums[0] * nums[1];
            allResult = RndNums(4, 100, false);
            Random rnd = new Random();
            trueResult = rnd.Next(4);
            allResult.SetValue(result, trueResult);
            var data = string.Format(Resources.Plugins.Trivia_Question, nums[0], nums[1], "\n");
            for (int i = 0; i < allResult.Length; i++)
            {
                data += ($"{i + 1}) {allResult[i]}   ");
            }
            return data;
        }

        private int[] RndNums(int numResult, int maxOfNum, bool repeat)
        {
            Random random = new Random();
            int[] resultRnd = new int[numResult];
            for (int i = 0; i < numResult; i++)
            {
                bool chekRepeat = true;
                do
                {
                    chekRepeat = true;
                    resultRnd[i] = random.Next(maxOfNum);
                    if (!repeat)
                    {

                        if (resultRnd[i] == result)
                        {
                            chekRepeat = false;
                        }

                        for (int ii = 0; ii < i; ii++)
                        {
                            if (resultRnd[ii] == resultRnd[i])
                            {
                                chekRepeat = false;
                            }

                        }
                    }
                }
                while (!chekRepeat);
            }
            return resultRnd;
        }
    }
}