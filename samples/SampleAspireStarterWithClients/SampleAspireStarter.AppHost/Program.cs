//#define __BRAINSTORMING__

IDistributedApplicationBuilder builder;

IResourceBuilder<ProjectResource> apiservice;
IResourceBuilder<ProjectResource> app_maui;
IResourceBuilder<ProjectResource> app_web;


builder = DistributedApplication.CreateBuilder(args);

apiservice = builder.AddProject<Projects.SampleAspireStarter_ApiService>("apiservice");

builder
    .AddProject<Projects.SampleAspireStarter_Web>("webfrontend")
    .WithReference(apiservice)
    ;

builder
    .AddProject<Projects.Client_AppConsole_Consumer>("client_app_console_consumer")
    .WithReference(apiservice)
    ;
builder
    .AddProject<Projects.Client_AppConsole_Producer>("client_app_console_producer")
    // localhost:PORT           possible
    // IP_PRIVATE_NETWORK:PORT  possible??
    .WithReference(apiservice)
    ;
builder
    .AddProject<Projects.Client_AppAvalonia_MVVM>("client_app_avalonia_mvvm")
    .WithReference(apiservice)
    ;

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
