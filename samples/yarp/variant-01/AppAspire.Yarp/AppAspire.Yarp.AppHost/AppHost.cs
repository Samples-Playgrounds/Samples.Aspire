using Aspire.Hosting;
using Aspire.Hosting.ApplicationModel;
// using Aspire.Hosting.Azure;
// using Aspire.Hosting.Docker;
using Microsoft.Extensions.DependencyInjection;


IDistributedApplicationBuilder builder = DistributedApplication.CreateBuilder(args);

IResourceBuilder<ProjectResource> yarp;
yarp = builder
            .AddProject<Projects.AppAspire_YarpProxy>("yarp-proxy")
            // Use the built‑in ServiceDefaults to get health checks, logs, etc.
            //.WithServiceDefaults()
            ;

IResourceBuilder<ProjectResource> web1;
web1 = builder
            .AddProject<Projects.AppBlazor_WebHomePage>("blazor-web1")
            // .WithServiceDefaults()
            // Expose the HTTP endpoint to YARP via an *internal* port.
            // The port is allocated by Aspire at runtime, and we expose it as a
            // reference for the proxy.
            .WithHttpEndpoint(port: 0, targetPort: 5001, name: "http1")
            ;

IResourceBuilder<ProjectResource> web2;
web2 = builder
            .AddProject<Projects.AppBlazor_WebPh4ct3x>("blazor-web2")
            // .WithServiceDefaults()
            // Expose the HTTP endpoint to YARP via an *internal* port.
            // The port is allocated by Aspire at runtime, and we expose it as a
            // reference for the proxy.
            .WithHttpEndpoint(port: 0, targetPort: 5002, name: "http2")
            ;

yarp
    .WithReference(web1.GetEndpoint("http1"))
    .WithEnvironment
    (
        context => 
        {
            // Replace the placeholder address in appsettings.json with the real URL.
            // YARP supports environment‑variable expansion: ${BLZ1_URL}
            context.EnvironmentVariables["BLZ1_URL"] = web1.GetEndpoint("http1").Url;
        }
    );

yarp.WithReference(web2.GetEndpoint("http2"))
    .WithEnvironment(context => {
        context.EnvironmentVariables["BLZ2_URL"] = web2.GetEndpoint("http2").Url;
    });

builder.Build().Run();
