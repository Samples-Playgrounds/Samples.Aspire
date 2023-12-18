 using Microsoft.Extensions.Configuration;
 using Microsoft.Extensions.DependencyInjection;
 using Microsoft.Extensions.Hosting;
 using Microsoft.Extensions.Logging;

 class Program
{
    static void Main(string[] args)
    {
        using 
        (
            IHost host = Host.CreateDefaultBuilder(args)
                                .ConfigureAppConfiguration
                                        (
                                            cfg =>
                                            {
                                                cfg.AddJsonFile("appsettings.json");
                                            }
                                        )
                                .ConfigureServices
                                        (
                                            (context, services) =>
                                            {
                                                // services.AddDbContext<EquitiesDbContext>
                                                // (
                                                //     options
                                                //     => 
                                                //     {
                                                //         options.UseSqlServer
                                                //                     (
                                                //                         context
                                                //                             .Configuration
                                                //                                 .GetConnectionString("Equities")
                                                //                     ); 
                                                //     }
                                                // );
                                                // services.AddScoped<ProgramAsync>();
                                            }
                                        )
                                .ConfigureLogging
                                        (
                                            (context, cfg) 
                                            =>
                                            {
                                                cfg.ClearProviders();
                                                cfg.AddConfiguration(context.Configuration.GetSection("Logging"));
                                                cfg.AddConsole();
                                            }
                                        )
                                .Build()
        )
        {
            using( IServiceScope scope = host.Services.CreateScope() )
            {
                // ProgramAsync p = scope.ServiceProvider.GetRequiredService<ProgramAsync>();
                // p.MainAsync().Wait();
            }
        }
        Console.WriteLine("Done.");
    }
}