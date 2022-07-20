using Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public interface IDal
    {
        /// <param name="userId">Phone number etc.</param>
        void SavePluginData(string userId, string pluginId, string data);
        /// <param name="userId">Phone number etc.</param>
        string LoadPluginData(string userId, string pluginId);
    }
}

