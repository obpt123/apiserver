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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

        }

        private IEnumerable<Assembly> GetWebApiAssemblies(IServiceCollection services)
        {
            var env = services.BuildServiceProvider().GetService<IHostingEnvironment>();
            var rootPath = System.IO.Path.Combine(env.ContentRootPath, "Services");
            if (Directory.Exists(rootPath))
            {
                return from p in Directory.GetFiles(rootPath, "*.webapi.dll", SearchOption.AllDirectories)
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
        public AppServiceProvider(IServiceCollection services)
        {
            
            this.provider=services.BuildServiceProvider();
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




}
