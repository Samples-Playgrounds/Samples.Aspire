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
    .AddProject<Projects.Client_AppAvalonia_MVVM>
                                    (
                                        "client_app_avalonia_mvvm"
                                    )
    .WithReference(apiservice);

builder
    .AddProject<Projects.Client_AppAvalonia_XPlat_Browser>
                                    (
                                        "client_app_avalonia_xplat_browser"
                                    )
    .WithReference(apiservice);

builder
    .AddProject<Projects.Client_AppAvalonia_XPlat_Desktop>
                                    (
                                        "client_app_avalonia_xplat_desktop"
                                    )
    .WithReference(apiservice);


#if __BRAINSTORMING__


builder
    .AddProject<Projects.Clients.Client_AppMAUI>("client_app_maui")
    // pass to MAUI
    // localhost:PORT           not possible
    // IP_PRIVATE_NETWORK:PORT  possible??
    .WithReference(apiservice)
    ;
#endif

builder.Build().Run();
