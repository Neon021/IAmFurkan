using Blog.Helpers.Startup;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using My3rdBlog;
using System.Threading.Tasks;

namespace Blog
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            Task<bool> roleResult = IdentityHelper.InitAsync(host);
            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}