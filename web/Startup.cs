using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using web.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;


using web.Models; // dodamo knjiznice
using Microsoft.AspNetCore.Identity; 

namespace web
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
            services.AddControllersWithViews();

            //Register the ResExpertContext as a Service 
            services.AddDbContext<ResExpertContext>(options =>
                                options.UseSqlServer(Configuration.GetConnectionString("AzureResExpert"))); // tukaj lahko spremenimo na lokalno

            // dodamo se to z stepom 6 
            //dodamo to dvoje not pa tko definiramo 
            services.AddIdentity<ApplicationUser, IdentityRole>(options => 
            options.Stores.MaxLengthForKeys = 128)
            .AddEntityFrameworkStores<ResExpertContext>()
            .AddDefaultUI()
            .AddDefaultTokenProviders();        
            services.AddSwaggerGen();      //dodano za Api dokumentacijo      
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication(); // dodamo not pri Autentikaciji
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                
                endpoints.MapRazorPages(); /// dodamo end point za razer page za predizdelane login page etc
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/api/v1/swagger.json", "My API V1");
            });
        }
    }
}
