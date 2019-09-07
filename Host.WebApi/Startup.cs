using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Runtime.Loader;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Internal;
using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;
using Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Console;

namespace Host.WebApi
{

    public class Startup
    {
        public static readonly LoggerFactory MyLoggerFactory
    = new LoggerFactory(new[] { new ConsoleLoggerProvider((_, __) => true, true) });
        public Startup(IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            Configuration = configuration;
            LoggerFactory = loggerFactory;
            this.RegisterDbContextHandlers();
        }
        public void RegisterDbContextHandlers()
        {
            DbContextOptionsBuilderHandlers.Register("sqlserver", (initContext, builer) => {
                builer.UseSqlServer(initContext.ConnectionString, (option) =>
                {
                    var migrationAssemblyName = initContext.GetMigrationAssemblyName();
                    if (!string.IsNullOrEmpty(migrationAssemblyName))
                    {
                        option.MigrationsAssembly(migrationAssemblyName);
                    }

                }).UseLoggerFactory(MyLoggerFactory);
            });
            DbContextOptionsBuilderHandlers.Register("sqlite", (initContext, builder) =>
            {
                builder.UseSqlite(initContext.ConnectionString, (option) =>
                {
                    var migrationAssemblyName = initContext.GetMigrationAssemblyName();
                    if (!string.IsNullOrEmpty(migrationAssemblyName))
                    {
                        option.MigrationsAssembly(migrationAssemblyName);
                    }
                }).UseLoggerFactory(MyLoggerFactory);
            });
        }
        public IConfiguration Configuration { get; }
        public ILoggerFactory LoggerFactory { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var assemblies = new string[] {"*.ef","*.impl" };
            services.AddAssemblies(assemblies, (assembly) =>
            {
                assembly.AppendOptions(services, Configuration)
                        .AppendServices(services, Configuration)
                        .AppendDbContexts(services, Configuration)
                        .AppendLoadServices(services,Configuration);

            }).AddMvc(mvcoption=> {
                mvcoption.ModelBinderProviders.Insert(0, new System.AspnetCore.QueryModelBinderProvider());

            });

            

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                loggerFactory.AddDebug();
            }
            app.ApplicationServices.MigrateDbContextDatabaseAsync("*.ef");
            app.UseMvc();

        }


       

    }




}





