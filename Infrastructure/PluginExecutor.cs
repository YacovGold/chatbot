using BasePlugin.Interfaces;
using BasePlugin.Records;
using Dal;
using System;
using System.Linq;
using System.Reflection;
using static Infrastructure.ServiceFactory;

namespace Infrastructure
{
    class CallbacksProxy : ICallbacks
    {
        public Action StartSession { get; set; }
        public Action EndSession { get; set; }
        public Action<string> SendMessage { get; set; }
        public Action<string> SavePluginUserData { get; set; }
    }

    public class PluginExecutor
    {
        const string SESSION_PLUGIN_ID = "SESSION_PLUGIN_ID";

        private readonly IDal _dal;
        private readonly PluginsMenu _pluginsMenu;
        private readonly PluginsManager _pluginsManager;
        private readonly ImplementationFactory _factory;

        public PluginExecutor(IDal dal, PluginsMenu pluginsMenu, PluginsManager pluginsManager, ImplementationFactory factory)
        {
            _dal = dal;
            _pluginsMenu = pluginsMenu;
            _pluginsManager = pluginsManager;
            _factory = factory;
        }

        public string GetCurrentUserPluginId(User user)
        {
            var currentPluginId = _dal.LoadPluginData(user.Id, SESSION_PLUGIN_ID);
            return currentPluginId;
        }

        public void Run(string message, User user)
        {
            var currentPluginId = GetCurrentUserPluginId(user);
            if (currentPluginId == null)
            {
                string msgForUser;
                if (CheckIfUserAskForHelp(message, out msgForUser)
                    || CheckIfIlegalPluginPressed(message, out int pluginNumber, out msgForUser))
                {
                    _factory(user.RunnerType).SendMessage(user.Id, msgForUser);
                    return;
                }
                var extraData = ExtractExtraData(message);
                var pluginType = ExtractPluginType(pluginNumber);
                Execute(pluginType, extraData, user);
            }
            else
            {
                Execute(currentPluginId, message, user);
            }
        }

        private string ExtractPluginType(int pluginNumber)
        {
            return _pluginsManager.GetPlugins()[pluginNumber - 1];
        }

        private string ExtractExtraData(string message)
        {
            return String.Join(' ', message.Split(' ').Skip(1).ToList());
        }

        private bool CheckIfIlegalPluginPressed(string message, out int pluginNumber, out string res)
        {
            var pluginIdFromUser = message.Split(' ')[0];
            var numOfPlugins = _pluginsManager.GetPlugins().Count;
            res = string.Empty;
            pluginNumber = 0;

            if (!int.TryParse(pluginIdFromUser, out pluginNumber))
            {
                res = "This option is not recognized, please type help to see the options.";
                return true;
            }
            if (pluginNumber > numOfPlugins || pluginNumber <= 0)
            {
                res = $"You only allowed to press number between 1 and {numOfPlugins}.";
                return true;
            }
            return false;
        }

        private bool CheckIfUserAskForHelp(string message, out string res)
        {
            res = string.Empty;
            if (message.ToLower() == "help")
            {
                res = _pluginsMenu.PluginsHelp();
                return true;
            }
            return false;
        }

        private void Execute(string pluginId, string input, User user)
        {
            IService service = _factory(user.RunnerType);
            var callbacks = new CallbacksProxy
            {
                StartSession = () => _dal.SavePluginData(user.Id, SESSION_PLUGIN_ID, pluginId),
                EndSession = () => _dal.SavePluginData(user.Id, SESSION_PLUGIN_ID, null),
                SendMessage = message => service.SendMessage(user.Id, message),
                SavePluginUserData = data => _dal.SavePluginData(user.Id, pluginId, data),
            };

            try
            {
                var plugin = _pluginsManager.GetPlugin(pluginId);
                var persistentData = _dal.LoadPluginData(user.Id, pluginId);
                plugin.Execute(new PluginInput(input, persistentData, callbacks));
            }
            catch (Exception)
            {
                service.SendMessage(user.Id, "An error occured while executing the plugin, please type help again");
                var plugin = new PluginInput(input, null, callbacks);
                plugin.Callbacks.EndSession();
            }
        }
    }
}
