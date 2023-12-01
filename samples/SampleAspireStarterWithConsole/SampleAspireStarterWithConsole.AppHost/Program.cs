IDistributedApplicationBuilder builder;

IResourceBuilder<ProjectResource> apiservice;

builder = DistributedApplication.CreateBuilder(args);


apiservice = builder.AddProject<Projects.SampleAspireStarterWithConsole_ApiService>("apiservice");

builder
    .AddProject<Projects.SampleAspireStarterWithConsole_Web>("webfrontend")
    .WithReference(apiservice);

builder
    .AddProject<Projects.AppConsole>("console")
    .WithReference(apiservice);

builder.Build().Run();
