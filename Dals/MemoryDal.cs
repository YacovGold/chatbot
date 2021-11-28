using Dal;
using System;
using System.Collections.Generic;

namespace Dals
{
    public class MemoryDal : IDal
    {
        Dictionary<string, string> _map = new();

        public void SaveData(string userData, string pluginId, string data)
        {
            _map[CreateKey(userData, pluginId)] = data;
        }

        public string LoadData(string userData, string pluginId)
        {
            var key = CreateKey(userData, pluginId);

            if (_map.ContainsKey(key))
            {
                return _map[key];
            }

            return null;
        }

        string CreateKey(string userData, string pluginId) => $"{userData}__{pluginId}";
    }
}
