using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// --- Logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// --- Configure Aspire authentication here
// Example placeholder: replace with actual Aspire registration
// builder.Services.AddAuthentication("Aspire")
//     .AddScheme<AspireOptions, AspireHandler>("Aspire", options => { /* configure */ });

//builder.Services.AddAuthorization();

// --- Forwarded headers so YARP sees correct scheme/host when behind a proxy/load-balancer
builder.Services.Configure<ForwardedHeadersOptions>
                            (
                                options =>
                                {
                                    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor 
                                                               |
                                                               ForwardedHeaders.XForwardedProto;
                                    // Accept forwarded headers from any proxy (adapt for production)
                                    options.KnownNetworks.Clear();
                                    options.KnownProxies.Clear();
                                }
                            );

// --- Add YARP and load routes/clusters from configuration
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

WebApplication app = builder.Build();

app.UseForwardedHeaders();

app.UseRouting();

// Ensure authentication/authorization are in the pipeline before proxy mapping
//app.UseAuthentication();
//app.UseAuthorization();

// Optional: expose health or diagnostics endpoints before the proxy
app.MapGet
        (
            "/health", 
            () => Results.Ok("healthy")
        );

// Map reverse proxy
app.MapReverseProxy();

app.Run();