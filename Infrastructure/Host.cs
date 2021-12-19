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
        private IDal _dal;
        private PluginsMenu _pluginsMenu;
        private PluginsManager _pluginsManager;

        public Host(IDal dal, PluginsMenu pluginsMenu, PluginsManager pluginsManager)
        {
            _dal = dal;
            _pluginsMenu = pluginsMenu;
            _pluginsManager = pluginsManager;
        }

        public string Run(string input, string user)
        {
            if (input.ToLower() == "help")
            {
                return _pluginsMenu.PlaginsHelp();
            }

            if (!int.TryParse(input, out int pluginNumber))
            {
                return "This option is not recognized, please type help to see the options.";
            }

            if (pluginNumber > PluginsManager.plugins.Count || pluginNumber <= 0)
            {
                return $"You only allowed to press number between 1 and {PluginsManager.plugins.Count}.";
            }

            var pluginId = PluginsManager.plugins[pluginNumber - 1];
            var plugin = _pluginsManager.CreatePlugin(pluginId);
            var session = _dal.LoadData(user, pluginId);

            var output = plugin.Execute(input, session, null);
            _dal.SaveData(user, pluginId, output.Session);

            return output.Message;
        }
    }
}
