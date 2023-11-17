var builder = DistributedApplication.CreateBuilder(args);

var apiservice = builder.AddProject<Projects.SampleAspireStarter_ApiService>("apiservice");

builder.AddProject<Projects.SampleAspireStarter_Web>("webfrontend")
    .WithReference(apiservice);

builder.Build().Run();
