using System.Windows;

namespace Client.AppWPF;

internal static class Program
{
    [STAThread]
    public static void Main()
    {
        /*
            Original Windows.Forms code:
            
            No Program.cs
            
            obj/$CONFIG/App.g.i.cs
                        
            App class has BuildAction       - ApplicationDefinition
            
            App class change BuildAction    - Page
            
            [STAThread]
            public static void Main()
            {
                App application = new App();
                application.InitializeComponent();
                application.Run();
            }
            
            https://www.codeproject.com/Questions/5355633/Add-main-to-a-WPF-application        
        */
        // -------------------------------------------------------------------------------------------------------------
        // Hosting
        //  start
        HostApplicationBuilder builder = Host.CreateApplicationBuilder();

        builder.AddAppDefaults();

        string scheme = builder.Environment.IsDevelopment() ? "http" : "https";
        builder.Services.AddHttpClient<WeatherApiClient>(client => client.BaseAddress = new($"{scheme}://apiservice"));

        builder.Services.AddSingleton<App>();
        builder.Services.AddSingleton<MainWindow>();

        IHost appHost = builder.Build();
        App app = appHost.Services.GetRequiredService<App>();
        Window window = appHost.Services.GetRequiredService<MainWindow>();
        
        appHost.Start();
        app.Run(window);

        appHost.StopAsync().GetAwaiter().GetResult();
        //  stop
        // Hosting
        // -------------------------------------------------------------------------------------------------------------
    }
}
