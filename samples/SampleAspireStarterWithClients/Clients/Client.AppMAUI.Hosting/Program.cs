#if IOS || MACCATALYST
using ObjCRuntime;
using UIKit;
#endif

using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Client.AppMAUI.Hosting;

internal static class Program
{
    // This is the main entry point of the application.
    public static void Main(string[] args)
    {


        // Register your appsettings.json config file
        IConfigurationRoot configuration = new ConfigurationBuilder()
                                                            .AddJsonFile
                                                                (
                                                                    Path.Combine("Properties", "appsettings.json"), 
                                                                    optional: true, 
                                                                    reloadOnChange: true
                                                                )
                                                            .Build();

        // Create a service provider registering the service discovery and HttpClient extensions
        ServiceProvider provider = new ServiceCollection()
                                                .AddServiceDiscovery()
                                                .AddHttpClient()
                                                .AddSingleton<IConfiguration>(configuration)
                                                .ConfigureHttpClientDefaults
                                                    (
                                                        static http =>
                                                        {
                                                            // Configure the HttpClient to use service discovery
                                                            http.UseServiceDiscovery();
                                                        }
                                                    )
                                                .BuildServiceProvider();

        // Grab a new client from the service provider
        HttpClient client = provider.GetService<HttpClient>()!;

        // Call an API called `apiservice` using service discovery
        HttpResponseMessage response = await client.GetAsync("http://apiservice/weatherforecast");
        string body = await response.Content.ReadAsStringAsync();

        Console.WriteLine(body);

        // -------------------------------------------------------------------------------------------------------------
        // Hosting
        //  start
        HostApplicationBuilder builder = Host.CreateApplicationBuilder();

        builder.AddAppDefaults();
        
        string scheme = builder.Environment.IsDevelopment() ? "http" : "https";
        Uri endpoint = new($"{scheme}://apiservice");
        builder.Services.AddHttpClient<Client.Services.WeatherApiClient>(client => client.BaseAddress = endpoint);

        // WPF
        /*
        builder.Services.AddSingleton<App>();
        builder.Services.AddSingleton<MainPage>();
        */
        

        IHost app_host = builder.Build();

        // WinForms
        /*
        */
        Services = app_host.Services;
        app_host.Start();

        // WPF
        /*
        App app = app_host.Services.GetRequiredService<App>();
        Page page = app_host.Services.GetRequiredService<MainPage>();
        
        app_host.Start();
        app.Run(page);
       */

        //  stop
        // Hosting
        // -------------------------------------------------------------------------------------------------------------

        /*
            Original MAUI code:

            No Program.cs
        */

        #if ANDROID
        Microsoft.Maui.Hosting.MauiApp app_maui = CreateMauiApp();
        #endif

        #if IOS
        // if you want to use a different Application Delegate class from "AppDelegate"
        // you can specify it here.
        UIApplication.Main(args, null, typeof(AppDelegate));
        #endif

        #if MACCATALYST
        // if you want to use a different Application Delegate class from "AppDelegate"
        // you can specify it here.
        UIApplication.Main(args, null, typeof(AppDelegate));
        #endif
        
        app_host.StopAsync().GetAwaiter().GetResult();

        return;
    }

    public static IServiceProvider Services { get; private set; } = default!;

    public static Microsoft.Maui.Hosting.MauiApp CreateMauiApp()
    {
        return MauiProgram.CreateMauiApp();
    }
}