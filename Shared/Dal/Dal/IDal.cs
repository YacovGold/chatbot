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
        /// <param name="userData">Phone number etc.</param>
        void SaveData(string userData, string pluginId, string data);
        /// <param name="userData">Phone number etc.</param>
        string LoadData(string userData, string pluginId);
    }
}

