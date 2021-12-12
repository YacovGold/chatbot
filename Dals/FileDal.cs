using Dal;
using System;
using System.IO;

namespace Dals
{
    public class FileDal : IDal
    {
        public void SaveData(string userData, string pluginId, string data)
        {
            File.WriteAllText(CreatePath(userData, pluginId), data);
        }

        public string LoadData(string userData, string pluginId)
        {
            var path = CreatePath(userData, pluginId);

            if (File.Exists(path))
            {
                return File.ReadAllText(path);
            }

            return null;
        }
 
        string CreatePath(string userData, string pluginId) => $"./Data/{pluginId}/{userData}.txt";
    }
}
