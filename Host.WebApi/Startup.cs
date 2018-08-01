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

namespace Host.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            var mvc = services.AddMvc();
            foreach (var ass in GetWebApiAssemblies(services))
            {
                mvc.AddApplicationPart(ass);

            }

            AppService.Provider = new AppServiceProvider(services);
            AppConfig.Provider = new AppConfigProvider(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }

            app.UseMvc();

        }


        private IEnumerable<Assembly> GetWebApiAssemblies(IServiceCollection services)
        {
            var env = services.BuildServiceProvider().GetService<IHostingEnvironment>();
            var rootPath = System.IO.Path.Combine(env.ContentRootPath, "Services");
            if (Directory.Exists(rootPath))
            {
                return from p in Directory.GetFiles(rootPath, "*.dll", SearchOption.AllDirectories)
                       select Assembly.LoadFrom(p);
            }
            else
            {
                return Enumerable.Empty<Assembly>();
            }

        }

    }

    public class AppServiceProvider : IAppServiceProvider
    {
        IServiceProvider provider;
        private Dictionary<string, string> types = new Dictionary<string, string>()
        {
            {
                "Service.IkeyValueService,KeyValueService.Core",
                "Service.Services.KeyValueService,KeyValueService.EF"
            }
        };
        public AppServiceProvider(IServiceCollection services)
        {
            //var ty = typeof(Service.IkeyValueService);
            foreach (var kv in types)
            {
                Type type = Type.GetType(kv.Key);
                Type implType = Type.GetType(kv.Value);
                ServiceDescriptor descriptor = new ServiceDescriptor(type, implType, ServiceLifetime.Singleton);

                services.Add(descriptor);
            }
            this.provider = services.BuildServiceProvider();


            

        }
        public T Get<T>(Type hostType, bool required = true)
        {
            if (required)
            {
                return provider.GetRequiredService<T>();
            }
            else
            {
                return provider.GetService<T>();
            }

        }


    }

    public class AppConfigProvider : IAppConfigProvider
    {
        private IConfiguration configuration;
        public AppConfigProvider(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string GetConfig(Type hostType, string configKey, bool required = true)
        {
            string val = configuration[configKey];
            return val;
        }

        public string GetConnString(Type hostType, string connectionName)
        {
            return configuration.GetConnectionString(connectionName);
        }
    }


}
