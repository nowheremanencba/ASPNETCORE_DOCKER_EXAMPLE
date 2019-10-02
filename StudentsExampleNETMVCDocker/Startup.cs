using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StudentsExampleNETMVCDocker.Models;

namespace StudentsExampleNETMVCDocker
{
    public class Startup
    {
        public static string DockerHostMachineIpAddress => Dns.GetHostAddresses(new Uri("http://docker.for.win.localhost").Host)[0].ToString();
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

    
        public void ConfigureServices(IServiceCollection services)
        { 
            // Add framework services.
            services.AddMvc();

            var host = Configuration["DBHOST"] ?? "192.168.1.99//SQLEXPRESS";
            var db = Configuration["DBNAME"] ?? "StudentsDB";
            var port = Configuration["DBPORT"] ?? "1433";
            var username = Configuration["DBUSERNAME"] ?? "sa";
            var password = Configuration["DBPASSWORD"] ?? "123456";

            string connStr = $"Data Source={host},{port};Integrated Security=False;";
            connStr += $"User ID={username};Password={password};Database={db}";
            connStr += $"Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            services.AddDbContext<StudentsContext>(options => options.UseSqlServer(connStr));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
