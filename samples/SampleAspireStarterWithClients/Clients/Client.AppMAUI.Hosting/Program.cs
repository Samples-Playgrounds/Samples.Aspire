#if IOS || MACCATALYST
using ObjCRuntime;
using UIKit;
#endif

namespace Client.AppMAUI.Hosting;

internal static class Program
{
    // This is the main entry point of the application.
    public static void Main(string[] args)
    {
        // -------------------------------------------------------------------------------------------------------------
        // Hosting
        //  start
        HostApplicationBuilder builder = Host.CreateApplicationBuilder();

        builder.AddAppDefaults();
        
        string scheme = builder.Environment.IsDevelopment() ? "http" : "https";
        Uri endpoint = new($"{scheme}://apiservice");
        builder.Services.AddHttpClient<Client.Services.WeatherApiClient>(client => client.BaseAddress = endpoint);

        builder.Services.AddSingleton<App>();

        IHost app = builder.Build();
        Services = app.Services;
        app.Start();
        //  stop
        // Hosting
        // -------------------------------------------------------------------------------------------------------------

        /*
            Original MAUI code:

            No Program.cs
        */

        #if ANDROID
        MauiApp app = CreateMauiApp();
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
        
        app.StopAsync().GetAwaiter().GetResult();

        return;
    }

    public static IServiceProvider Services { get; private set; } = default!;

    public static MauiApp CreateMauiApp()
    {
        return MauiProgram.CreateMauiApp();
    }
}