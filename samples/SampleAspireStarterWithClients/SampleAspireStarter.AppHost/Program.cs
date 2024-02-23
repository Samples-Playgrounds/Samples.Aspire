//#define __BRAINSTORMING__

IDistributedApplicationBuilder builder;

IResourceBuilder<ProjectResource> apiservice;
IResourceBuilder<ProjectResource> app_maui;
IResourceBuilder<ProjectResource> app_web;


builder = DistributedApplication.CreateBuilder(args);

apiservice = builder.AddProject<Projects.SampleAspireStarter_ApiService>("apiservice");

builder
    .AddProject<Projects.SampleAspireStarter_Web>
                                    (
                                        "webfrontend"
                                    )
    .WithReference(apiservice);





builder
    .AddProject<Projects.Client_AppConsole>
                                    (
                                        "client_app_console"
                                    )
    .WithReference(apiservice);

builder
    .AddProject<Projects.Client_AppConsole_Configuration>
                                    (
                                        "client_app_console_configuration"
                                    )
    .WithReference(apiservice);

builder
    .AddProject<Projects.Client_AppConsole_Configuration_NotNot_AppSettings>
                                    (
                                        "client_app_console_configuration_notnot_appsettings"
                                    )
    .WithReference(apiservice);

builder
    .AddProject<Projects.Client_AppConsole_InfiniteLoop>
                                    (
                                        "client_app_console_infinite_loop"
                                    )
    .WithReference(apiservice);

builder
    .AddProject<Projects.Client_AppConsole_With_OpenTelemetry>
                                    (
                                        "client_app_console_with_opentelemetry"
                                    )
    .WithReference(apiservice);

builder
    .AddProject<Projects.Client_AppConsole_SelfHosted_InfiniteLoop>
                                    (
                                        "client_app_console_self_hosted_infinite_loop"
                                    )
    // localhost:PORT           possible
    // IP_PRIVATE_NETWORK:PORT  possible??
    .WithReference(apiservice);

builder
    .AddProject<Projects.Client_AppConsole_SelfHosted_With_Configuration>
                                    (
                                        "client_app_console_selfhosted_with_configuration"
                                    )
    .WithReference(apiservice);


builder
    .AddProject<Projects.Client_AppConsole_Microsoft_Extensions>
                                    (
                                        "client_app_console_microsoft_extensions"
                                    )
    .WithReference(apiservice);




builder
    .AddProject<Projects.Client_AppAvaloniaUI>              // NOTE: strongly typed (using generic version)
                                    (
                                        "client_app_avalonia"
                                    )
    .WithReference(apiservice);

builder
    .AddProject<Projects.Client_AppAvaloniaUI_MVVM>         // NOTE: strongly typed (using generic version)
                                    (
                                        "client_app_avalonia_mvvm"
                                    )
    .WithReference(apiservice);


builder
    .AddProject<Projects.Client_AppAvaloniaUI_XPlat_Desktop>
                                    (
                                        "client_app_avalonia_xplat_desktop"
                                    )
    .WithReference(apiservice);

builder
    .AddProject<Projects.Client_AppAvaloniaUI_XPlat_Browser>
                                    (
                                        "client_app_avalonia_xplat_browser"
                                    )
    .WithReference(apiservice);

#if WINDOWS
/*
    
*/
/* 
    Register the client apps by project path as they target a TFM incompatible with the AppHost,
    so can't be added as regular project references (see the AppHost.csproj file for additional 
    metadata added to the ProjectReference to coordinate a build dependency though).
*/
builder
    .AddProject
        (
            "client_app_winforms", 
            // path relative to the project not execution folder
            "../Clients/Client.AppWinForms/Client.AppWinForms.csproj"
        )
    .WithReference(apiservice);

builder
    .AddProject
        (
            "client_app_wpf", 
            // path relative to the project not execution folder
            "../Clients/Client.AppWPF/Client.AppWPF.csproj"
        )
    .WithReference(apiservice);
#endif


//#if __BRAINSTORMING__
builder
    .AddProject                                                 // NOTE: not strongly typed (not using generic version)
            (
                "client_app_maui",
                "../Clients/Client.AppMAUI/Client.AppMAUI.csproj"
            )
            .WithReference(apiservice)
            ;
/*
*/
builder
    .AddProject                                                 // NOTE: not strongly typed (not using generic version)
            (
                "client_app_maui_hosting",
                "../Clients/Client.AppMAUI.Hosting/Client.AppMAUI.Hosting.csproj"
            )
            .WithReference(apiservice)
            ;

builder.Build().Run();
