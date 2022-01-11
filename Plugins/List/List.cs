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

        public PluginOutput Execute(PluginInput input)
        {
            List<string> list = new();

            if (string.IsNullOrEmpty(input.PersistentData) == false)
            {
                list = JsonSerializer.Deserialize<PersistentDataStructure>(input.PersistentData).List;
            }

            if (input.Message == "")
            {
                input.Callbacks.StartSession();
                return new PluginOutput("List started. Enter 'Add' to add task. Enter 'Delete' to delete task. Enter 'List' to view all list. Enter 'Exit' to stop.",input.PersistentData);
            }
            else if (input.Message.ToLower() == "exit")
            {
                input.Callbacks.EndSession();
                return new PluginOutput("List stopped.",input.PersistentData);
            }
            else if (input.Message.ToLower().StartsWith("add"))
            {
                string str=input.Message.Substring(3);
                list.Add (str);

                var data = new PersistentDataStructure(list);

                return new PluginOutput($"New task: {str}", JsonSerializer.Serialize(data));
            }
            else if (input.Message.ToLower() == "delete")
            {
                string str=input.Message.Substring(6);
                list = list.Where(task => task != str).ToList();
                
                var data = new PersistentDataStructure(list);

                return new PluginOutput($"Delete task: {str}", JsonSerializer.Serialize(data));
            }
            else if (input.Message.ToLower() == "list")
            {
                string listtasks = string.Join("\n", list);
                return new PluginOutput($"All list tasks: {listtasks}/r/n",input.PersistentData);
            }
            else
            {
                return new PluginOutput("Error! Enter 'Add' to add task. Enter 'Delete' to delete task. Enter 'List' to view all list. Enter 'Exit' to stop.");
            }
        }

      
    }
}
