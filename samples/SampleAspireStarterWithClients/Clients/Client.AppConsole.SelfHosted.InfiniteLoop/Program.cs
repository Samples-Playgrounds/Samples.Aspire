
using System.Net.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

Microsoft.Extensions.Hosting.HostApplicationBuilder builder;

builder = Microsoft.Extensions.Hosting.Host
                                        .CreateApplicationBuilder(args);

// Add service defaults & Aspire components.
builder.AddServiceDefaults();

/*
TODO: 
    apiservice
    localhost:5393
*/

// HttpClient client = new ()                                        
//                         {
//                             client.BaseAddress = new("http://apiservice");
//                         };


for (int i=1; ; i++ )
{
    Console.WriteLine($"Console launched by Aspire and rolling...");
    Console.WriteLine($"    Hello, World! {i}");
    
    Thread.Sleep(1000);
}

return;