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
using Serilog;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc.Razor;
using System.Globalization;
using Microsoft.AspNetCore.Localization;

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

            #region 映射model及viewModel
            //https://www.cjavapy.com/article/100/
            //Install-Package AutoMapper -Version 9.0.0
            //Install-Package AutoMapper.Extensions.Microsoft.DependencyInjection -Version 7.0.0
            //Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            #endregion


            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });


            #region 將資料庫entities所需的設定注入
            IConfiguration _config = GetSettings();

            //將Settings.json的資訊注入
            services.Configure<Settings>(_config);

            //相依性注入取得NorthwindContext物件
            services.AddDbContext<Northwind.Entities.Models.NorthwindContext>(options =>
                  options.UseSqlServer(_config.GetConnectionString("NorthwindConnection")));
            #endregion


            //注入 Serilog
            services.AddSingleton(Serilog.Log.Logger);


            //https://blog.johnwu.cc/article/ironman-day04-asp-net-core-dependency-injection.html
            //Transient
            //如預期，每次注入都是不一樣的實例。
            //Scoped
            //在同一個 Requset 中，不論是在哪邊被注入，都是同樣的實例。
            //Singleton
            //不管 Requset 多少次，都會是同一個實例。
            //相依注入GenericRepository
            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));           
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IProductsDAL, ProductsDAL>();
            services.AddTransient<IProductsService, ProductsService>();
            services.AddTransient<ICustomerDAL, CustomerDAL>();
            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<ILoginService, LoginService>();


            //加入驗證token
            //https://blog.miniasp.com/post/2019/10/13/How-to-use-JWT-token-based-auth-in-aspnet-core-22
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                // 當驗證失敗時，回應標頭會包含 WWW-Authenticate 標頭，這裡會顯示失敗的詳細錯誤原因
                options.IncludeErrorDetails = true; // 預設值為 true，有時會特別關閉

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    // 透過這項宣告，就可以從 "sub" 取值並設定給 User.Identity.Name
                    NameClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier",
                    // 透過這項宣告，就可以從 "roles" 取值，並可讓 [Authorize] 判斷角色
                    RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role",

                    // 一般我們都會驗證 Issuer
                    ValidateIssuer = true,
                    ValidIssuer = _config.GetSection("Tokens").GetSection("ValidIssuer").Value, // "JwtAuthDemo" 應該從 IConfiguration 取得

                    // 通常不太需要驗證 Audience
                    ValidateAudience = false,
                    //ValidAudience = "JwtAuthDemo", // 不驗證就不需要填寫

                    // 一般我們都會驗證 Token 的有效期間
                    ValidateLifetime = true,

                    // 如果 Token 中包含 key 才需要驗證，一般都只有簽章而已
                    ValidateIssuerSigningKey = false,

                    // 應該從 IConfiguration 取得
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_config.GetSection("Tokens").GetSection("IssuerSigningKey").Value))
                     
                };
            });


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



            //加入Serilog，要在UseStaticFiles的上面
            app.UseSerilogRequestLogging();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseAuthentication();//必須加在usermvc前

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

                if (env.IsDevelopment() || env.IsProduction())
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
