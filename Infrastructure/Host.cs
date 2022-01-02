using BasePlugin.Interfaces;
using BasePlugin.Records;
using Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class Host
    {
        const string ACTIVE_SESSION = "active session plugin";

        private readonly IDal _dal;
        private readonly PluginsMenu _pluginsMenu;
        private readonly PluginsManager _pluginsManager;

        private readonly Callbacks _callbacks = new();

        public Host(IDal dal, PluginsMenu pluginsMenu, PluginsManager pluginsManager)
        {
            _dal = dal;
            _pluginsMenu = pluginsMenu;
            _pluginsManager = pluginsManager;
        }

        public string Run(string message, string user)
        {
            var currentPluginId = _dal.LoadData(user, ACTIVE_SESSION);
            if (currentPluginId == null)
            {
                if (message.ToLower() == "help")
                {
                    return _pluginsMenu.PlaginsHelp();
                }

                if (!int.TryParse(message, out int pluginNumber))
                {
                    return "This option is not recognized, please type help to see the options.";
                }

                if (pluginNumber > PluginsManager.plugins.Count || pluginNumber <= 0)
                {
                    return $"You only allowed to press number between 1 and {PluginsManager.plugins.Count}.";
                }

                var pluginId = PluginsManager.plugins[pluginNumber - 1];
                return Execute(pluginId, string.Empty, user);
            }
            else
            {
                return Execute(currentPluginId, message, user);
            }
        }

        private string Execute(string pluginId, string input, string user)
        {
            _callbacks.StartSession = () => _dal.SaveData(user, ACTIVE_SESSION, pluginId);
            _callbacks.EndSession = () => _dal.SaveData(user, ACTIVE_SESSION, null);

            var plugin = _pluginsManager.CreatePlugin(pluginId);
            var persistentData = _dal.LoadData(user, pluginId);
            var output = plugin.Execute(new PluginInput( input, persistentData, _callbacks));

            _dal.SaveData(user, pluginId, output.PersistentData);
            return output.Message;
        }
    }
}