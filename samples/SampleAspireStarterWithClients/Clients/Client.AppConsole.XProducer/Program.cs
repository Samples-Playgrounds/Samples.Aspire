﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

public class Program
{
    public static void Main(string[] args)
    {
        IServiceCollection services = new ServiceCollection();
        // Startup.cs finally :)
        Startup startup = new Startup();
        startup.ConfigureServices(services);
        IServiceProvider serviceProvider = services.BuildServiceProvider();

        //configure console logging
        serviceProvider
            .GetService<ILoggerFactory>()
            //.AddConsole(LogLevel.Debug)
            ;

        ILogger<Program> logger = serviceProvider
                                        .GetService<ILoggerFactory>()
                                        .CreateLogger<Program>();

        logger.LogDebug("Logger is working!");

        // Get Service and call method
        IMyService? service = serviceProvider
                                    .GetService<IMyService>();
        service.MyServiceMethod();
    }
}