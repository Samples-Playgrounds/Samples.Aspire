using HW.Aspire.MAUI;

IDistributedApplicationBuilder builder = DistributedApplication.CreateBuilder(args);

string prefix_servers = "../../ServersBackends/";
string prefix_clients = "../../Clients/";

IResourceBuilder<ProjectResource> app_web_blazor;
app_web_blazor = builder.AddProject//<Projects.AppWeb_Blazor>
                                        (
                                        "app-web-blazor",
                                            $"{prefix_servers}/AppWeb.Blazor/AppWeb.Blazor.csproj"
                                        );
IResourceBuilder<ProjectResource> app_web_blazor_fluent;
app_web_blazor_fluent = builder.AddProject//<Projects.AppWeb_Blazor_Fluent>
                                        (
                                            "app-web-blazor-fluent",
                                            $"{prefix_servers}/AppWeb.Blazor.Fluent/AppWeb.Blazor.Fluent.csproj"
                                        );
IResourceBuilder<ProjectResource> app_web_blazor_server_fluentui;
app_web_blazor_server_fluentui = builder.AddProject//<Projects.AppWeb_Blazor_Server_FluentUI>
                                            (
                                                "app-web-blazor-server-fluentui",
                                                $"{prefix_servers}/AppWeb.Blazor.Server.FluentUI/AppWeb.Blazor.Server.FluentUI.csproj"
                                            );
IResourceBuilder<ProjectResource> app_web_blazor_wasm;
app_web_blazor_wasm = builder.AddProject
                                            (
                                                "app-web-blazor-wasm",
                                                $"{prefix_servers}/AppWeb.Blazor.WASM/AppWeb.Blazor.WASM.csproj"
                                            );
IResourceBuilder<ProjectResource> app_web_blazor_wasm_empty;
app_web_blazor_wasm_empty = builder.AddProject
                                            (
                                                "app-web-blazor-wasm-empty",
                                                $"{prefix_servers}/AppWeb.Blazor.WASM.Empty/AppWeb.Blazor.WASM.Empty.csproj"
                                            );
IResourceBuilder<ProjectResource> app_web_blazor_wasm_fluentui;
app_web_blazor_wasm_fluentui = builder.AddProject
                                            (
                                                "app-web-blazor-wasm-fluentui",
                                                $"{prefix_servers}/AppWeb.Blazor.WASM.FluentUI/AppWeb.Blazor.WASM.FluentUI.csproj"
                                            );
IResourceBuilder<ProjectResource> app_web_mvc;
app_web_mvc = builder.AddProject
                                            (
                                                "app-web-mvc",
                                                $"{prefix_servers}/AppWeb.MVC/AppWeb.MVC.csproj"
                                            );
IResourceBuilder<ProjectResource> app_web_razor;
app_web_razor = builder.AddProject
                                            (
                                                "app-web-razor",
                                                $"{prefix_servers}/AppWeb.Razor/AppWeb.Razor.csproj"
                                            );
IResourceBuilder<ProjectResource> app_web_razor_empty;
app_web_razor_empty = builder.AddProject
                                            (
                                                "app-web-razor-empty",
                                                $"{prefix_servers}/AppWeb.Razor.Empty/AppWeb.Razor.Empty.csproj"
                                            );
/*
IResourceBuilder<ProjectResource> app_web_wasm;
app_web_wasm = builder.AddProject
                                            (
                                                "app-web-wasm",
                                                $"{prefix}/AppWeb.WASM/AppWeb.WASM.csproj"
                                            );
*/

IResourceBuilder<ProjectResource> serviceapi_rest_webapi;
serviceapi_rest_webapi = builder.AddProject
                                            (
                                                "serviceapi-rest-webapi",
                                                $"{prefix_servers}/ServiceAPI.REST.WebAPI/ServiceAPI.REST.WebAPI.csproj"
                                            );

IResourceBuilder<ProjectResource> serviceapi_rest_webapi_aot;
serviceapi_rest_webapi_aot = builder.AddProject
                                            (
                                                "serviceapi-rest-webapi-aot",
                                                $"{prefix_servers}/ServiceAPI.REST.WebAPI.AOT/ServiceAPI.REST.WebAPI.AOT.csproj"
                                            );

IResourceBuilder<ProjectResource> serviceapi_grpc;
serviceapi_grpc = builder.AddProject
                                            (
                                                "serviceapi-grpc",
                                                $"{prefix_servers}/ServiceAPI.gRPC/ServiceAPI.gRPC.csproj"
                                            );


IResourceBuilder<ProjectResource> client_maui;


client_maui = builder.AddProject
                                            (
                                                "client-maui",
                                                $"{prefix_clients}/AppClient.MAUI/AppClient.MAUI.csproj"
                                            )
                                            .WithReference(serviceapi_rest_webapi)
                                            .WithReference(serviceapi_rest_webapi_aot)
                                            .WithReference(serviceapi_grpc)
                                            .GenerateSettings()
                                            ;

var builder_build = builder.Build();

builder_build.Run();

return;