using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using Raven.Client.Documents;
using Raven.DependencyInjection;
using RavenDb.Tryouts.Api.Extensions;
using RavenDb.Tryouts.DataLayer.Models;
using RavenDb.Tryouts.DataLayer.Repositories;

namespace RavenDb.Tryouts.Api
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddCors()
                .AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                });

            services.AddRavenDbDocStore();
            services.AddRavenDbAsyncSession();

            services.AddScoped<IGenericRepository<RavenItem>, GenericRepository<RavenItem>>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            
            var docStore = app.ApplicationServices.GetRequiredService<IDocumentStore>();
            docStore.EnsureExists();
        }
    }
}