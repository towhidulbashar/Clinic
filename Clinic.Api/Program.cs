using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;

namespace Clinic.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Error()
                .MinimumLevel.Is(LogEventLevel.Error)
                .Enrich.FromLogContext()
                .WriteTo.File("log.txt", LogEventLevel.Error, "{Timestamp:dddd, dd MMM yyyy hh:mm:fff tt } [{Level}] {Message}{NewLine}{Exception}{NewLine}{NewLine}")
                .CreateLogger();
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)                
                .UseStartup<Startup>()
                .UseSerilog()
                .Build();
    }
}
