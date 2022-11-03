using BasePlugin;
using BasePlugin.Interfaces;
using BasePlugin.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace List
{
    record PersistentDataStructure(List<string> List);

    public class ListPlugin : IPlugin
    {
        public const string _Id = "list";
        public string Id => _Id;

        public void Execute(PluginInput input)
        {
            List<string> list = new();

            if (string.IsNullOrEmpty(input.PersistentData) == false)
            {
                list = JsonSerializer.Deserialize<PersistentDataStructure>(input.PersistentData).List;
            }

            if (input.Message == "")
            {
                input.Callbacks.StartSession();
                input.Callbacks.SavePluginUserData(input.PersistentData);
                input.Callbacks.SendMessage(Resources.Plugins.List_Welcome);
            }
            else if (input.Message.ToLower() == "exit")
            {
                input.Callbacks.EndSession();
                input.Callbacks.SavePluginUserData(input.PersistentData);
                input.Callbacks.SendMessage(Resources.Plugins.List_Stopped);
            }
            else if (input.Message.ToLower().StartsWith("add"))
            {
                var str = input.Message.Substring("add".Length).Trim();
                list.Add(str);

                var data = new PersistentDataStructure(list);

                input.Callbacks.SavePluginUserData(JsonSerializer.Serialize(data));
                input.Callbacks.SendMessage(string.Format(Resources.Plugins.List_NewTask, str));
            }
            else if (input.Message.ToLower().StartsWith("delete"))
            {
                var str = input.Message.Substring("delete".Length).Trim();
                list = list.Where(task => task != str).ToList();

                var data = new PersistentDataStructure(list);

                input.Callbacks.SavePluginUserData(JsonSerializer.Serialize(data));
                input.Callbacks.SendMessage(string.Format(Resources.Plugins.List_DeleteTask, str));
            }
            else if (input.Message.ToLower() == "list")
            {
                string listtasks = string.Join("\r\n", list);
                input.Callbacks.SavePluginUserData(input.PersistentData);
                input.Callbacks.SendMessage(string.Format(Resources.Plugins.List_AllList, "\r\n"+listtasks));
            }
            else
            {
                input.Callbacks.SavePluginUserData(input.PersistentData);
                input.Callbacks.SendMessage(Resources.Plugins.List_ErorIncorrectInput);
            }
        }
    }
}
