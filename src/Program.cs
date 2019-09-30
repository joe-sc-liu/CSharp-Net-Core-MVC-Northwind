using System;
using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;

//加入Serilog參考 https://github.com/serilog/serilog-aspnetcore
//https://github.com/serilog/serilog-aspnetcore/tree/dev/samples/EarlyInitializationSample
//https://derekarends.com/asp-net-core-2-2-and-global-logger/
//http://www.coding.nctu.me/%E7%8D%A8%E7%AB%8B%E4%BD%BF%E7%94%A8-serilog-in-asp-net-core-2-0/
//只要裝這三個Install-Package Serilog.AspNetCore   Serilog.Sinks.Async   Serilog.Sinks.RollingFile


namespace CSharp_Net_Core_MVC_Northwind
{
    public class Program
    {
        public static IConfiguration SerilogConfiguration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            //如果有分開發環境或生產環境，可以加上以下設定
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        public static void Main(string[] args)
        {
            #region serilog設定
            Log.Logger = new LoggerConfiguration()
            //info跟系統的Warning寫入檔案
            .ReadFrom.Configuration(SerilogConfiguration)
            //debug寫入clonsole
            .Enrich.FromLogContext()
            .WriteTo.Debug()
            .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}")
            .CreateLogger();
            #endregion

            try
            {
                CreateWebHostBuilder(args).Build().Run();
            }
            catch (SystemException e)
            {
                Log.Error(e, "Host terminated unexpectedly");
                throw;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                //註冊 Serilog
                .UseSerilog();
    }
}
