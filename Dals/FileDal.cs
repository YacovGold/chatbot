using Dal;
using System;
using System.IO;

namespace Dals
{
    public class FileDal : IDal
    {
        public void SavePluginData(string userId, string pluginId, string data)
        {
            File.WriteAllText(CreatePath(userId, pluginId), data);
        }

        public string LoadPluginData(string userId, string pluginId)
        {
            var path = CreatePath(userId, pluginId);

            if (File.Exists(path))
            {
                return File.ReadAllText(path);
            }

            return null;
        }
 
        string CreatePath(string userData, string pluginId) => $"./Data/{pluginId}/{userData}.txt";
    }
}
