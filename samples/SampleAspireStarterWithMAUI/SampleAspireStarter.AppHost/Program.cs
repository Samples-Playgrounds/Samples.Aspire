#define __BRAINSTORMING__

IDistributedApplicationBuilder builder;

IResourceBuilder<ProjectResource> apiservice;
IResourceBuilder<ProjectResource> app_maui;
IResourceBuilder<ProjectResource> app_web;


// 
builder = DistributedApplication.CreateBuilder(args);


apiservice = builder
                .AddProject<Projects.SampleAspireStarter_ApiService>("apiservice");

app_maui = builder
                .AddProject<Projects.SampleMAUI>("app_maui")
                #if __BRAINSTORMING__
                .Run(new string[] { "maccatalyst", "ios" } )
                .Run("android")
                .MsBuildParams("ios", "")
                #endif
                ;
app_web = builder
                .AddProject<Projects.SampleAspireStarter_Web>("app_web_frontend")
                .WithReference(apiservice)
                ;

builder.Build()
        .Run()
        #if __BRAINSTORMING__
        .Run(new string[] { "maccatalyst", "ios" } )
        .Run("android")
        #endif
        ;
