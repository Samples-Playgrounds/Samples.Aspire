/*
Original source from template

var builder = DistributedApplication.CreateBuilder(args);

var apiservice = builder.AddProject<Projects.SampleAspireStarter_ApiService>("apiservice");

builder.AddProject<Projects.SampleAspireStarter_Web>("webfrontend")
    .WithReference(apiservice);
*/

IDistributedApplicationBuilder builder;

IResourceBuilder<ProjectResource> apiservice;
IResourceBuilder<ProjectResource> webfrontend;

builder = DistributedApplication.CreateBuilder(args);


apiservice = builder
                .AddProject<Projects.SampleAspireStarter_ApiService>("apiservice");

webfrontend = builder
                .AddProject<Projects.SampleAspireStarter_Web>("webfrontend")
                .WithReference(apiservice);




/*
    Dependencies

        Clients

            non ASP.net clients (not ASP.net Blazor WASM)
*/
builder
    .AddProject<Projects.AppConsole>("console")
    .WithReference(apiservice);
builder
    .AddProject<Projects.AppConsoleSelfHosted>("console_self_hosted")
    .WithReference(apiservice);



builder.Build().Run();
