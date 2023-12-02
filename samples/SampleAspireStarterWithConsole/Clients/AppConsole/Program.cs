
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

Microsoft.Extensions.Hosting.HostApplicationBuilder builder;

builder = Microsoft.Extensions.Hosting.Host
                                        .CreateApplicationBuilder(args);
builder
    .Services
        .AddHostedService<AppConsole.WeatherClientService>()
        .AddHttpClient<AppConsole.WeatherApiClient>
                                    (
                                        client
                                        => 
                                        {
                                            client.BaseAddress = new("http://apiservice");
                                        }
                                    )
                ;

Microsoft.Extensions.Hosting.IHost host = builder.Build();
host.Run();
