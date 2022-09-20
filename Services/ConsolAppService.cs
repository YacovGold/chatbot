using BasePlugin.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ConsolAppService : IService
    {
        //public void SendMessage(string userId, string data)
        //{
        //    Console.WriteLine(data);
        //}
        public void SendMessage(string userId, string data)
        {
            var first = 0;
            var last = 0;
            var ret = "";

            for (int i = 0; i < data.Length; i++)
            {
                var c = data[i];
                if (IsHebrewText(c) == false)
                {
                    ret += c;
                    continue;
                }
                else // Heb start here
                {
                    first = i;
                    while (true)
                    {
                        if (i > data.Length - 1)
                        {
                            last = i;
                            break;
                        }
                        c = data[i];

                        if ((c == ' ') && (!IsHebrewText(data[i + 1])))
                        {
                            last = i;
                            break;
                        }

                        if (CheckWhatCharThis(c) == false)
                        {
                            last = i;
                            break;
                        }
                        i++;
                    }
                    i--;

                    var toi = last - first;
                    var heb = data.Substring(first, toi);
                    var n = Reverse(heb);
                    ret += n;
                }
            }
            Console.WriteLine(ret);
        }

        static bool CheckWhatCharThis(char c)
        {
            if (IsHebrewText(c))
            {
                return true;
            }

            if (c == ' ')
            {
                return true;
            }

            if (char.IsSymbol(c))
            {
                return true;
            }

            if (c == '\"')
            {
                return true;
            }

            return false;
        }
        static bool IsHebrewText(char c)
        {
            var result = c <= 'ת' && c >= 'א';
            return result;
        }
        static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}
