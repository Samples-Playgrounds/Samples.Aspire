using Aspire.Hosting;
using Aspire.Hosting.ApplicationModel;
using Aspire.Hosting.Docker;
using Aspire.Hosting.Kubernetes;


IDistributedApplicationBuilder builder = DistributedApplication.CreateBuilder(args);

IResourceBuilder<DockerComposeEnvironmentResource> env_docker_compose;
IResourceBuilder<KubernetesEnvironmentResource> env_k8s_cluster;

IResourceBuilder<ProjectResource> yarp;
IResourceBuilder<ProjectResource> web1;
IResourceBuilder<ProjectResource> web2;


env_docker_compose = builder.AddDockerComposeEnvironment("docker-compose");
env_k8s_cluster = builder.AddKubernetesEnvironment("k8s-cluster");

web1 = builder
            .AddProject<Projects.AppBlazor_WebHomePage>("blazor-web1")
            //.WithServiceDefaults()
            // Expose the HTTP endpoint to YARP via an *internal* port.
            // The port is allocated by Aspire at runtime, and we expose it as a
            // reference for the proxy.
            .WithHttpEndpoint(port: 0, targetPort: 8121, name: "http1")
            ;

web2 = builder
            .AddProject<Projects.AppBlazor_WebPh4ct3x>("blazor-web2")
            //.WithServiceDefaults()
            // Expose the HTTP endpoint to YARP via an *internal* port.
            // The port is allocated by Aspire at runtime, and we expose it as a
            // reference for the proxy.
            .WithHttpEndpoint(port: 0, targetPort: 8221, name: "http2")
            ;

yarp = builder
            .AddProject<Projects.AppAspire_YarpReverseProxy>("yarp-proxy")
            // Use the built‑in ServiceDefaults to get health checks, logs, etc.
            //.WithServiceDefaults()
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
    .WithEnvironment
    (
        context => 
        {
            context.EnvironmentVariables["BLZ2_URL"] = web2.GetEndpoint("http2").Url;
        }
    );

// https://learn.microsoft.com/en-us/dotnet/aspire/whats-new/dotnet-aspire-9.2#-deployment-improvements

//builder.AddDocker();

builder.Build().Run();
