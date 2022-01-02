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
        private readonly IDal _dal;
        private readonly PluginsMenu _pluginsMenu;
        private readonly PluginsManager _pluginsManager;

        private readonly Callbacks _callbacks = new();
        private IPlugin _currentPlugin;

        public Host(IDal dal, PluginsMenu pluginsMenu, PluginsManager pluginsManager)
        {
            _dal = dal;
            _pluginsMenu = pluginsMenu;
            _pluginsManager = pluginsManager;
        }

        public string Run(string message, string user)
        {
            if (_currentPlugin == null)
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
                var plugin = _pluginsManager.CreatePlugin(pluginId);
                return Execute(plugin, pluginId, "", user);
            }
            else
            {
                var plugin = _currentPlugin;
                var pluginId = _currentPlugin.Id;
                return Execute(plugin, pluginId, message, user);
            }
        }

        private string Execute(IPlugin plugin, string pluginId, string message, string user)
        {
            _callbacks.StartSession = () => _currentPlugin = plugin;
            _callbacks.EndSession = () => _currentPlugin = null;

            var persistentData = _dal.LoadData(user, pluginId);
            var output = plugin.Execute(new PluginInput(message, persistentData, _callbacks));
            _dal.SaveData(user, pluginId, output.persistentData);

            return output.Message;
        }
    }
}
