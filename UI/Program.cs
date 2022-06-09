using Microsoft.Extensions.DependencyInjection;
using Services.Services;

namespace UI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var services = Startup.ConfigureServices();
            var serviceProvider = services.BuildServiceProvider();

            serviceProvider.GetService<Game>().Run();
        }
    }
}