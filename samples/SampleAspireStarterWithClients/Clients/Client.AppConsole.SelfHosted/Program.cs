Microsoft.Extensions.Hosting.HostApplicationBuilder builder;
Microsoft.Extensions.Hosting.IHost host;

builder = Microsoft.Extensions.Hosting
                    .Host
                        .CreateApplicationBuilder(args);
builder
    .Services
        .AddHostedService<AppConsoleSelfHosted.WeatherClientService>()
        .AddHttpClient<AppConsoleSelfHosted.WeatherApiClient>
                                    (
                                        client
                                        => 
                                        {
                                            client.BaseAddress = new("http://apiservice");
                                        }
                                    )
        ;

host  = builder.Build();
host.Run();
