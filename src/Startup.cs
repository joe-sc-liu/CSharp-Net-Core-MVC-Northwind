using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using Northwind.Util;
using Microsoft.EntityFrameworkCore;
using Northwind.DAL;
using Northwind.DAL.Interfaces;
using Northwind.Service;
using Northwind.Service.Interfaces;


namespace CSharp_Net_Core_MVC_Northwind
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });


            IConfiguration _config = GetSettings();
            string connstr = _config.GetConnectionString("NorthwindConnection");

            //�NSettings.json����T�`�J
            services.Configure<Settings>(_config);

            //�̩ۨʪ`�J���oNorthwindContext����
            services.AddDbContext<Northwind.Entities.Models.NorthwindContext>(options =>
                  options.UseSqlServer(_config.GetConnectionString("NorthwindConnection")));


            //https://blog.johnwu.cc/article/ironman-day04-asp-net-core-dependency-injection.html
            //Transient
            //�p�w���A�C���`�J���O���@�˪���ҡC
            //Scoped
            //�b�P�@�� Requset ���A���׬O�b����Q�`�J�A���O�P�˪���ҡC
            //Singleton
            //���� Requset �h�֦��A���|�O�P�@�ӹ�ҡC
            //�̪ۨ`�JGenericRepository
            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));


           
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IProductsDAL, ProductsDAL>();
            services.AddTransient<IProductsService, ProductsService>();


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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }

        private IConfigurationRoot GetSettings()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "Configuration"))
                .AddJsonFile(path: "Settings.json", optional: true, reloadOnChange: true);
            return builder.Build();
        }
    }
}
