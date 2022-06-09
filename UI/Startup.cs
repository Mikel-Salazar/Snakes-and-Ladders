using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.Services;

namespace UI
{
    public static class Startup
    {
        public static IServiceCollection ConfigureServices()
        {
            var services = new ServiceCollection();

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            IConfiguration configuration = builder.Build();
            services.AddSingleton(configuration);

            services.AddSingleton<Game>();
            services.AddSingleton<IDiceService, DiceService>();
            services.AddSingleton<IPlayerService, PlayerService>();

            return services;
        }
    }
}
