using BlazorExpenseTracker2.UI.Interfaces;
using BlazorExpenseTracker2.UI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorExpenseTracker2.UI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            //por defecto nos muestra los errores en el navegador - > activamos para que nos lo muestre en el navegador
            services.AddServerSideBlazor().AddCircuitOptions(option => { option.DetailedErrors = true; });

            //inyeccion desde el servicio
            services.AddHttpClient<ICategoryService, CategoryService>
                (
                    //client hay que definir el cliente en este caso la API
                    //BaseAddress indicar que es una ubicacion
                    //Uri representa un objeto URL
                    //para saber la UTR nos vamos a la API, en Properties - launchSettings y aparece aca "applicationUrl": "http://localhost:6389" o "applicationUrl": "https://localhost:5001;http://localhost:5000" o "sslPort": 44362
                    client => { client.BaseAddress = new Uri("https://localhost:44362"); }
                );
            //injeccion para la categoria de expenses
            services.AddHttpClient<IExpenseService, ExpenseService>
                (
                    client => { client.BaseAddress = new Uri("https://localhost:44362"); }
                );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
