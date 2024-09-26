using Blog.Helpers.Startup;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using MyBlog;
using System.Threading.Tasks;

namespace Blog
{
    public class Program
    {
        public static void Main(string[] args)
        {
	var host = CreateWebHostBuilder(args)
		    .UseKestrel()
		    .UseUrls("http://*:5001")
		    .UseStartup<Startup>()
		    .Build();	
            Task<bool> roleResult = IdentityHelper.InitAsync(host);
            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
