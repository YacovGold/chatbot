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

        public Host(IDal dal) => _dal = dal;

        public string Run(string input, string user)
        {
            if (!int.TryParse(input, out int pluginNumber) || pluginNumber > PluginsManager.plugins.Count || pluginNumber <= 0)
            {
                return new PluginsMenu().PlaginsHelp();
            }

            var pluginId = PluginsManager.plugins[pluginNumber - 1];
            var plugin = new PluginsManager().CreatePlugin(pluginId);
            var session = _dal.LoadData(user, pluginId);

            var output = plugin.Execute(input, session, null);
            _dal.SaveData(user, pluginId, output.Session);

            return output.Message;
        }
    }
}
