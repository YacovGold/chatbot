using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal;
using DB;
using DB.Data;
using DB.Models;
using Microsoft.EntityFrameworkCore;

namespace Dals
{
    public class DbDal : IDal
    {
        private readonly ChatbotContext _ctx;

        public DbDal(ChatbotContext ctx)
        {
            _ctx = ctx;
            _ctx.Database.EnsureCreated();
        }

        public string LoadPluginData(string userId, string pluginId)
        {
            var upd = _ctx.userPluginData.Find(userId, pluginId);
            return upd?.Data;
        }

        public void SavePluginData(string userId, string pluginId, string data)
        {
            var upd = _ctx.userPluginData.Find(userId, pluginId);

            if (upd is not null)
            {
                upd.Data = data;
                _ctx.Update(upd);
            }
            else
            {
                var userPluginData = new UserPluginData
                {
                    UserId = userId,
                    PluginId = pluginId,
                    Data = data
                };

                _ctx.Add(userPluginData);
            }

            _ctx.SaveChanges();  
        }
    }
}
