//using System;
//using System.Runtime.CompilerServices;
//using Microsoft.AspNetCore.Builder;

//[CompilerGenerated]
//internal class Program
//{
//    private static void _003CMain_003E_0024(string[] args)
//    {
//        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
//        WebApplication app = builder.Build();
//        app.MapGet("/", (Func<string>)(() => "Hello World!"));
//        app.Run();
//    }
//}

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WSServer
{
    public class Program
    {
        public static void Main(string[] args) => CreateHostBuilder(args).Build().Run();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
