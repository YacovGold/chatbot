using BasePlugin.Interfaces;
using Dal;
using Dals;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using List;  
using CountDown;
using DiceRoller;
using DB.Data;
using System.Diagnostics;
using BasePlugin.Services;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public static class Container
    {
        public delegate IService ImplementationFactory(RunnerType runner);

        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            var directory = Directory.GetCurrentDirectory();
            
            var plugins = Directory.GetFiles(directory, "*.dll", SearchOption.AllDirectories)
                .SelectMany(pluginDll =>
                {
                    try
                    {
                        if (new FileInfo(pluginDll).Length < 20_000)
                        {
                            return Assembly.LoadFrom(pluginDll).GetTypes();
                        }
                        else
                        {
                            return new Type[0];
                        }
                    }
                    catch (BadImageFormatException)
                    {
                        return new Type[0];
                    }
                })
            .Distinct()
            .Where(type => typeof(IPlugin).IsAssignableFrom(type) && !type.IsInterface)
            .ToList();

            services.AddScoped<IDal, DbDal>();
            services.AddDbContext<ChatbotContext>();
            services.AddScoped<IScheduler, Scheduler>();
            services.AddScoped<PluginsManager>();
            services.AddScoped<PluginsMenu>();
            services.AddScoped<PluginExecutor>();
            services.AddScoped<ConsolAppService>();
            services.AddScoped<TelegramService>();
            services.AddScoped<WhatsappService>();

            services.AddScoped<ImplementationFactory>(serviceProvider => runner =>
            {
                return runner switch
                {
                    RunnerType.ConsoleApp => serviceProvider.GetRequiredService<ConsolAppService>(),
                    RunnerType.Whatsapp => serviceProvider.GetRequiredService<WhatsappService>(),
                    RunnerType.Telegram => serviceProvider.GetRequiredService<TelegramService>(),
                    _ => throw new NotImplementedException()
                };
            });

            foreach (var plugin in plugins)
            {
                services.AddScoped(typeof(IPlugin), plugin);
            }

            return services;
        }

    }
}
