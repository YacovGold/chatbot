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
        public static string _Id = "list";
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
                input.Callbacks.SaveData(input.PersistentData);
                input.Callbacks.SendMessage("List started. Enter 'Add' to add task. Enter 'Delete' to delete task. Enter 'List' to view all list. Enter 'Exit' to stop.");
                return;
            }
            else if (input.Message.ToLower() == "exit")
            {
                input.Callbacks.EndSession();
                input.Callbacks.SaveData(input.PersistentData);
                input.Callbacks.SendMessage("List stopped.");
                return;
            }
            else if (input.Message.ToLower().StartsWith("add"))
            {
                var str = input.Message.Substring("add".Length).Trim();
                list.Add(str);

                var data = new PersistentDataStructure(list);

                input.Callbacks.SaveData(JsonSerializer.Serialize(data));
                input.Callbacks.SendMessage($"New task: {str}");
                return;
            }
            else if (input.Message.ToLower().StartsWith("delete"))
            {
                var str = input.Message.Substring("delete".Length).Trim();
                list = list.Where(task => task != str).ToList();

                var data = new PersistentDataStructure(list);

                input.Callbacks.SaveData(JsonSerializer.Serialize(data));
                input.Callbacks.SendMessage($"Delete task: {str}");
                return;
            }
            else if (input.Message.ToLower() == "list")
            {
                string listtasks = string.Join("\r\n", list);
                input.Callbacks.SaveData(input.PersistentData);
                input.Callbacks.SendMessage($"All list tasks:\r\n{listtasks}");
                return;
            }
            else
            {
                input.Callbacks.SaveData(input.PersistentData);
                input.Callbacks.SendMessage("Error! Enter 'Add' to add task. Enter 'Delete' to delete task. Enter 'List' to view all list. Enter 'Exit' to stop.");
                return;
            }
        }
    }
}
