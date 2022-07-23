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
        private readonly ChatbotContext ctx;

        public DbDal()
        {
            ctx = new ChatbotContext();
            ctx.Database.EnsureCreated();
        }

        public string LoadPluginData(string userId, string pluginId)
        {
            var upd = ctx.userPluginData.Find(userId, pluginId);
            return upd?.Data;
        }

        public void SavePluginData(string userId, string pluginId, string data)
        {
            var upd = ctx.userPluginData.Find(userId, pluginId);

            if (upd is not null)
            {
                upd.Data = data;
                ctx.Update(upd);
            }
            else
            {
                var userPluginData = new UserPluginData
                {
                    UserId = userId,
                    PluginId = pluginId,
                    Data = data
                };

                ctx.Add(userPluginData);
            }

            ctx.SaveChanges();  
        }
    }
}
