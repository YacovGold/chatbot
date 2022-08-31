using BasePlugin.Interfaces;
using BasePlugin.Records;
using Dal;
using System;
using System.Linq;

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
        private readonly IMessageSender _messageSender;
        private readonly PluginsMenu _pluginsMenu;
        private readonly PluginsManager _pluginsManager;

        public PluginExecutor(IMessageSender messageSender, IDal dal, PluginsMenu pluginsMenu, PluginsManager pluginsManager)
        {
            _dal = dal;
            _messageSender = messageSender;
            _pluginsMenu = pluginsMenu;
            _pluginsManager = pluginsManager;
        }
        public void Run(string message, string user)
        {
            var currentPluginId = _dal.LoadPluginData(user, SESSION_PLUGIN_ID);
            if (currentPluginId == null)
            {
                
                string msgForUser;
                if (CheckIfUserAskForHelp(message, out msgForUser)
                    || CheckIfIlegalPluginPressed(message, out int pluginNumber, out msgForUser))
                {
                    _messageSender.SendMessage(user, msgForUser);
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
            return PluginsManager.plugins[pluginNumber - 1];
        }

        private string ExtractExtraData(string message)
        {
            return String.Join(' ', message.Split(' ').Skip(1).ToList());
        }
        private bool CheckIfIlegalPluginPressed(string message, out int pluginNumber, out string res)
        {
            var pluginIdFromUser = message.Split(' ')[0];
            res = string.Empty;
            pluginNumber = 0;

            if (!int.TryParse(pluginIdFromUser, out pluginNumber))
            {
                res = "This option is not recognized, please type help to see the options.";
                return true;
            }
            if (pluginNumber > PluginsManager.plugins.Count || pluginNumber <= 0)
            {
                res = $"You only allowed to press number between 1 and {PluginsManager.plugins.Count}.";
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
        private void Execute(string pluginId, string input, string user)
        {
            var callbacks = new CallbacksProxy
            {
                StartSession = () => _dal.SavePluginData(user, SESSION_PLUGIN_ID, pluginId),
                EndSession = () => _dal.SavePluginData(user, SESSION_PLUGIN_ID, null),
                SendMessage = message => _messageSender.SendMessage(user, message),
                SavePluginUserData = data => _dal.SavePluginData(user, pluginId, data),
            };

            try
            {
                var plugin = _pluginsManager.CreatePlugin(pluginId);
                var persistentData = _dal.LoadPluginData(user, pluginId);
                plugin.Execute(new PluginInput(input, persistentData, callbacks));

            }
            catch (Exception)
            {
                _messageSender.SendMessage(user, "An error occured while executing the plugin, please type help again");
                var plugin= new PluginInput(input, null, callbacks);
                plugin.Callbacks.EndSession();
            }
        }
    }
}