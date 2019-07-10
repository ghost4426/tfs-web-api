using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace AdminWebApi
{

    public class Program
    {

        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args).UseKestrel().UseUrls("https://*:4200")
                .UseStartup<Startup>();
    }
}
