using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace linkedInTutorial
{
    public class Startup
    {
        private readonly IConfigurationRoot configuration = null;


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public Startup(IHostingEnvironment env)
        {

            configuration = new ConfigurationBuilder().AddEnvironmentVariables().AddJsonFile(env.ContentRootPath + "/config.json")
                                                                                 .AddJsonFile(env.ContentRootPath + "/config.development.json").Build();
            //configuration.GetValue
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<FeatureToggles>(x => new FeatureToggles
            {
                EnableDeveloperException =
                 configuration.GetValue<bool>("FeatureToggles:EnableDeveloperException")
            }
            );
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, FeatureToggles featureToggles)
        {

            //var configuration = new ConfigurationBuilder().AddEnvironmentVariables().AddJsonFile(env.ContentRootPath + "/config.json")
            //                                                                      .AddJsonFile(env.ContentRootPath + "/config.development.json").Build();

            // if (configuration.GetValue<bool>("FeatureToggles:EnableDeveloperException"))
            if (featureToggles.EnableDeveloperException)
            {
                app.UseDeveloperExceptionPage();
            }
            else
                app.UseExceptionHandler("/error.html");

            app.Use(async (context, next) =>
            {
                if (context.Request.Path.Value.Contains("invalid"))
                    throw new Exception("Error!");
                await next();
                //await context.Response.WriteAsync("Hello World!");
            });
            app.UseMvc(route =>
            {
                route.MapRoute
                ("Default", "{controller=home}/{action=Index}/{id?}"
                );
            });
            app.UseFileServer();
            //app.UseStaticFiles();
        }
    }
}
