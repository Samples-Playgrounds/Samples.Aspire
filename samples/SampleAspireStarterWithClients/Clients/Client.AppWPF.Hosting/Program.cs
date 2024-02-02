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
        Uri endpoint = new($"{scheme}://apiservice");
        builder.Services.AddHttpClient<Client.Services.WeatherApiClient>(client => client.BaseAddress = endpoint);

        builder.Services.AddSingleton<App>();
        builder.Services.AddSingleton<MainWindow>();

        IHost app_host = builder.Build();
        App app = app_host.Services.GetRequiredService<App>();
        Window window = app_host.Services.GetRequiredService<MainWindow>();
        
        app_host.Start();
        app.Run(window);

        app_host.StopAsync().GetAwaiter().GetResult();
        //  stop
        // Hosting
        // -------------------------------------------------------------------------------------------------------------
    }
}
