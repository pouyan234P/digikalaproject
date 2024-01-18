using digika_mobileapp.Services;
using digika_mobileapp.Services.IServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace digika_mobileapp
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
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder
                        .AllowAnyOrigin() // You might want to tighten this to specific origins
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });
            services.AddControllers();
            services.AddHttpClient<IProductService, ProductService>();
            SD.ProductApiBase = Configuration["ServiceUrls:ProductApi"];
            services.AddScoped<IProductService, ProductService>();
            services.AddHttpClient<IUserpointService, UserpointService>();
            services.AddScoped<IUserpointService, UserpointService>();
            services.AddHttpClient<IShoppingService,ShoppingService>();
            SD.ShoppingApiBase = Configuration["ServiceUrls:ShoppingApi"];
            services.AddScoped<IShoppingService, ShoppingService>();
            // Use Kestrel for hosting
            /*var options = new KestrelServerOptions();
            options.Listen<string>("http://*:5000");
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseKestrel(options);
                    webBuilder.UseStartup<Startup>();
                });*/
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "digika_mobileapp", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "digika_mobileapp v1"));
            }
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseHttpsRedirection();
         
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            /*app.UseKestrel(options =>
            {
                options.Listen(new IPEndPoint(IPAddress.Any, 5000));
            });*/
        }
    }
}
