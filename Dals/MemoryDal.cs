using Dal;
using System;
using System.Collections.Generic;

namespace Dals
{
    public class MemoryDal : IDal
    {
        Dictionary<string, string> _map = new();

        public void SavePluginData(string userId, string pluginId, string data)
        {
            _map[CreateKey(userId, pluginId)] = data;
        }

        public string LoadPluginData(string userId, string pluginId)
        {
            var key = CreateKey(userId, pluginId);

            if (_map.ContainsKey(key))
            {
                return _map[key];
            }

            return null;
        }

        string CreateKey(string userData, string pluginId) => $"{userData}__{pluginId}";
    }
}
