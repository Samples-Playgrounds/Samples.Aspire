using System.Diagnostics;
using Microsoft.Extensions.Logging;
using OpenTelemetry;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;
using OpenTelemetry.Resources;
using System.Diagnostics.Metrics;

using
    ILoggerFactory loggerFactory = LoggerFactory.Create
                                        (
                                            builder => 
                                            {
                                                builder.AddOpenTelemetry
                                                            (
                                                                options => 
                                                                {
                                                                   options.AddConsoleExporter();
                                                                }
                                                            );
                                            }
                                        );

ILogger<Program> logger = loggerFactory.CreateLogger < Program > ();
logger.LogInformation("Hello from OpenTelemetry");



Meter MyMeter = new("ConsoleDemo.Metrics", "1.0");

Counter<long> RequestCounter = MyMeter.CreateCounter<long>("RequestCounter");

using MeterProvider meterProvider = Sdk.CreateMeterProviderBuilder()
                                                .AddMeter("ConsoleDemo.Metrics")
                                                .AddConsoleExporter()
                                                .Build();

RequestCounter.Add(1, new KeyValuePair<string, object?>("POST Request", HttpMethod.Post));
RequestCounter.Add(1, new KeyValuePair<string, object?>("GET Request", HttpMethod.Get));
RequestCounter.Add(1, new KeyValuePair<string, object?>("GET Request", HttpMethod.Get));
RequestCounter.Add(1, new KeyValuePair<string, object?>("POST Request", HttpMethod.Post));
RequestCounter.Add(1, new KeyValuePair<string, object?>("PUT Request", HttpMethod.Put));


/*
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
*/
ActivitySource MyActivitySource = new("ConsoleDemo.Trace");
using
    TracerProvider tracer_provider = Sdk
                                        .CreateTracerProviderBuilder()
                                        .AddSource("ConsoleDemo.Trace")
                                        .AddConsoleExporter()
                                        .Build();

using(var activity = MyActivitySource.StartActivity("ActivityStarted")) 
{
    int start_number = 10000;
    activity?.SetTag("StartNumber", start_number);
    for (int i = 0; i < start_number; i++) 
    {
        DoProcess(i);
    }
    activity?.SetStatus(ActivityStatusCode.Ok);
}
void DoProcess(int currentNumber) 
{
    var doubleValue = currentNumber * 2;
}    
    
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