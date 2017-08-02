using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using CoreSimple.db;
using Microsoft.EntityFrameworkCore;
using CoreSimple.Model;
using CoreSimple.Service;
using CoreSimple.Logger;
using System.IO;

namespace CoreSimple
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<CoreContext>(options => options.UseSqlServer(connection));
            services.AddTransient<ServiceAsync<City>>();
            services.AddTransient<IService<City>, Service<City>>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory,CoreContext context)
        {
            var pathToFolder = Directory.GetCurrentDirectory() + "/ApiLog";
            var pathToFile = Path.Combine(pathToFolder, "logger.txt");

            Directory.CreateDirectory(pathToFolder);
            if (File.Exists(pathToFile))
            {
                File.Delete(pathToFile);
            }

            loggerFactory.AddFile(pathToFile);


            app.UseMvc();
            app.UseCors(config => config.AllowAnyHeader());
            DbInitializer.Initialize(context);
        }
    }
}
