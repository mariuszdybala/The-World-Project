﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microsoft.Framework.DependencyInjection;
using TheWorld.Services;
using Microsoft.Framework.Runtime;
using Microsoft.Extensions.Configuration;

namespace TheWorld
{
    public class Startup
    {
        public static IConfiguration Configuration;

        public Startup(IApplicationEnvironment aE)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(aE.ApplicationBasePath)
                .AddJsonFile("config.json")
                .AddEnvironmentVariables();

            Configuration = builder.Build();
            
        }

        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
#if DEBUG
            services.AddScoped<IMailService, DebugMailService>();
#else
            services.AddScoped<IMailService, RealMailService>();
#endif         
        }

        public void Configure(IApplicationBuilder app)
        {
            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
            app.UseMvc(config =>
            {
                config.MapRoute(
                    name: "Default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "App", action = "Index" }
                        );
            }
            );

        }
    }
}
