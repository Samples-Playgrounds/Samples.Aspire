using System.Diagnostics;
// See https://aka.ms/new-console-template for more information

using OpenTelemetry;
using OpenTelemetry.Trace;
using OpenTelemetry.Resources;

// ...

var serviceName = "Client.AppConsole.With.OpenTelemetry";
var serviceVersion = "1.0.0";

using TracerProvider tracerProvider = Sdk
                                        .CreateTracerProviderBuilder()
                                        .AddSource(serviceName)
                                        .SetResourceBuilder
                                                (
                                                    ResourceBuilder.CreateDefault()
                                                    .AddService
                                                            (
                                                                serviceName: serviceName, 
                                                                serviceVersion: serviceVersion
                                                            )
                                                )
                                        .AddConsoleExporter()
                                        .Build();


Console.WriteLine("Console.WriteLine - Hello, World! Launched by Aspire");
Trace.WriteLine("Trace.WriteLine - Hello, World! Launched by Aspire");