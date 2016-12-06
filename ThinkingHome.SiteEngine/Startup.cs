using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ThinkingHome.SiteEngine
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc(ConfigureRoutes);
            app.UseStaticFiles();
            app.UseStatusCodePages();
        }

        private void ConfigureRoutes(IRouteBuilder routes)
        {
            routes.MapRoute(
                name: "home",
                template: string.Empty,
                defaults: new { controller = "Home", action = "Index" });

            routes.MapRoute(
                name: "info",
                template: "info/{*path}",
                defaults: new { controller = "Home", action = "Info" },
                constraints: new { path = ".*[.]md" }
            );
        }
    }
}