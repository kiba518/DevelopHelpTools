using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    //Kestrel的默认监听端口是http5000、https5001。 
                    webBuilder.ConfigureKestrel(options =>
                    {
                        options.ListenAnyIP(5180);//监听本机任意ip的5180端口，相当于设置ip0.0.0.0
                        //options.Listen(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5180));//监听指定ip的指定端口
                    });;
                });
    }
}
