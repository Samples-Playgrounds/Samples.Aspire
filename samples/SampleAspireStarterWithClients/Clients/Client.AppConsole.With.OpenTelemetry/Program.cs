using System.Diagnostics;
// See https://aka.ms/new-console-template for more information

using OpenTelemetry;
using OpenTelemetry.Trace;
using OpenTelemetry.Resources;

// ...

string service_name = "Client.AppConsole.With.OpenTelemetry";
string service_version = "1.0.0";

using TracerProvider tracerProvider = Sdk
                                        .CreateTracerProviderBuilder()
                                        .AddSource(service_name)
                                        .SetResourceBuilder
                                                (
                                                    ResourceBuilder.CreateDefault()
                                                    .AddService
                                                            (
                                                                serviceName: service_name, 
                                                                serviceVersion: service_version
                                                            )
                                                )
                                        .AddConsoleExporter()
                                        .Build();


Console.WriteLine("Console.WriteLine - Hello, World! Launched by Aspire");
System.Diagnostics.Trace.WriteLine
                            (
                                "Trace.WriteLine"
                                + Environment.NewLine + 
                                "\t" +
                                "Console App With Open Telemetry"
                                + Environment.NewLine +
                                "\t\t" +
                                "Hello, World! Launched by Aspire"
                            );