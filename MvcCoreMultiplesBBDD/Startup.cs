using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MvcCoreMultiplesBBDD.Data;
using MvcCoreMultiplesBBDD.Repositories;
using MvcCoreMultiplesBBDD.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreMultiplesBBDD
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

            /*********************IMPORTANTE************************/
            string cadenaoracle = this.Configuration.GetConnectionString("hospitaloracle");
            string cadenasqlserver = this.Configuration.GetConnectionString("hospitalsqlserver");
            string cadenamysql = this.Configuration.GetConnectionString("hospitalmysql");

            //ACCESO A DATOS SQL SERVER *********************SQL SERVER
            //services.AddTransient<IRepositoryEmpleados, RepositoryEmpleadosSQL>();
            //services.AddDbContext<HospitalContext>(options => options.UseSqlServer(cadenasqlserver));

            //esta solucion es modificando el find details el filtro
            //services.AddDbContext<HospitalContext>(options => options.UseOracle(cadenaoracle));
            //SOLUCION PARA ORACLE CON EL FIND DETAILS, sustituyendo la de arriba es mejor esta solucion
            //esto es para *************************************ORACLE
            /*services.AddTransient<IRepositoryEmpleados, RepositoryEmpleadosOracle>();
            services.AddDbContext<HospitalContext>
            (options => options.UseOracle(cadenaoracle, options => options
            .UseOracleSQLCompatibility("11")));*/

            /*esto es para *******************************MYSQL*/

            services.AddTransient<IRepositoryEmpleados, RepositoryEmpleadosMysql>();
            services.AddDbContext<HospitalContext>(options => options.UseMySql(cadenamysql,ServerVersion.AutoDetect(cadenamysql)));
            

            services.AddControllersWithViews();
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
