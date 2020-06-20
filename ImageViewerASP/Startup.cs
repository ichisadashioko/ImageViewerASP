using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using ImageViewerASP.Services;

namespace ImageViewerASP
{
    public class Startup
    {
        public static IConfigurationRoot Configuration { get; private set; }
        public Startup(IHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDirectoryBrowser();
            services.AddMvc(option =>
            {
                option.EnableEndpointRouting = false;
            });

            // add functionality to inject IOptions<T>
            services.AddOptions();
            // add our Config object so it can be injected
            services.Configure<AppConfig>(Configuration.GetSection("AppConfig"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseMvc(routes =>
            {
                routes.MapRoute("Default", "{controller=Home}/{action=Index}/{requestId?}");
            });

            app.UseStaticFiles();

            string imageRoot = Configuration.GetValue<string>("AppConfig:ImagePath");
            string requestPath = Configuration.GetValue<string>("AppConfig:RequestPath");
            Debug.WriteLine($"AppConfig:ImagePath: {imageRoot}");
            Debug.WriteLine($"AppConfig:RequestPath: {requestPath}");

            var isImageRootExisted = Directory.Exists(imageRoot);

            if (!isImageRootExisted)
            {
                throw new ApplicationException($"{imageRoot} does not exist!");
            }

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(imageRoot),
                RequestPath = requestPath
            });

            app.UseDirectoryBrowser(new DirectoryBrowserOptions
            {
                FileProvider = new PhysicalFileProvider(imageRoot),
                RequestPath = requestPath
            });

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "Contents")
                ),
                RequestPath = "/Contents"
            });

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "Scripts")
                ),
                RequestPath = "/Scripts"
            });
        }
    }
}
