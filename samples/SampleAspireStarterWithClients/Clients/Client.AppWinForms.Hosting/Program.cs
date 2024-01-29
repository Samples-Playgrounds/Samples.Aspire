namespace Client.AppWinForms;

internal static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        /*
            Original Windows.Forms code:

         */

        // -------------------------------------------------------------------------------------------------------------
        // Hosting
        //  start
        HostApplicationBuilder builder = Host.CreateApplicationBuilder();

        builder.AddAppDefaults();

        string scheme = builder.Environment.IsDevelopment() ? "http" : "https";
        Uri endpoint = new($"{scheme}://apiservice");
        builder.Services.AddHttpClient<Client.Services.WeatherApiClient>(client => client.BaseAddress = endpoint);

        IHost app = builder.Build();
        Services = app.Services;
        app.Start();
        //  stop
        // Hosting
        // -------------------------------------------------------------------------------------------------------------

        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();
        Application.Run(ActivatorUtilities.CreateInstance<MainForm>(app.Services));

        app.StopAsync().GetAwaiter().GetResult();
    }

    public static IServiceProvider Services { get; private set; } = default!;
}
