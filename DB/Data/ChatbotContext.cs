using DB.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Net;

namespace DB.Data
{
    public class ChatbotContext : DbContext
    {
        public DbSet<UserPluginData> userPluginData { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseInMemoryDatabase("chatDB"); 
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserPluginData>()
                .HasKey(upd => new { upd.UserId, upd.PluginId });
        }
    }
}

