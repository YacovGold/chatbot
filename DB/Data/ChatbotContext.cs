using DB.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Net;
using System.Text.RegularExpressions;

namespace DB.Data
{
    public class ChatbotContext : DbContext
    {
        public DbSet<UserPluginData> userPluginData { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");

            if (connectionString != null)
            {
                if (connectionString.StartsWith("mysql:"))
                {
                    var newConnectionString = connectionString.Substring("mysql:".Length);
                    builder.UseMySQL(newConnectionString);
                }
                else if (connectionString.StartsWith("postgres:"))
                {
                    var m = Regex.Match(connectionString, @"postgres://(.*):(.*)@(.*):(.*)/(.*)");
                    builder.UseNpgsql($"Server={m.Groups[3]};Port={m.Groups[4]};User Id={m.Groups[1]};Password={m.Groups[2]};Database={m.Groups[5]};sslmode=Prefer;Trust Server Certificate=true");
                }
                else
                {
                    throw new NotImplementedException("The connection string is not recognized");
                }
            }
            else
            {
                builder.UseInMemoryDatabase("chatDB");
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserPluginData>()
                .HasKey(upd => new { upd.UserId, upd.PluginId });
        }
    }
}

