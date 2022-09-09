using BasePlugin.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasePlugin.Services
{
    public class ConsolAppService : IService
    {
        public void SendMessage(string userId, string data)
        {
            Console.WriteLine(data);
        }
    }
}
