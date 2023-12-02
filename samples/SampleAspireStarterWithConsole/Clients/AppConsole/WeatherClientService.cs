using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace AppConsole
{
    public class WeatherClientService : IHostedService
    {
        private readonly HttpClient http_client;

        // public WeatherClientService(HttpClient httpClient)
        // {
        //     http_client = httpClient;

        //     return;
        // }

        private readonly IHttpClientFactory http_client_factory;

        public WeatherClientService(IHttpClientFactory hcf)
        {
            http_client_factory = hcf;

            return;
        }

        private readonly WeatherClientService typed_service;

        // public WeatherClientService(WeatherClientService s)
        // {
        //     typed_service = s;

        //     return;
        // }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            DoWork1("http get from apiserivce");
            // DoWork2("http get from apiserivce");

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private async void DoWork1(object state)
        {
            string url = "http://localhost:5393/weatherforecast";

            for (int i=1; ; i++ )
            {
                Console.WriteLine($"=======================================================================");
                var request = new HttpRequestMessage
                                        (
                                            HttpMethod.Get, 
                                            url
                                        );
                
                var client = http_client_factory.CreateClient();
                var response = await client.SendAsync(request);

                Console.WriteLine($"Hello, World! {i}");
                Console.WriteLine($"{response.Content}");
            }            
        }

        private async void DoWork2(object state)
        {
            string url = "http://localhost:5393/weatherforecast";

            for (int i=1; ; i++ )
            {
                Console.WriteLine($"=======================================================================");
                var request = new HttpRequestMessage
                                        (
                                            HttpMethod.Get, 
                                            url
                                        );
                
                // var response = await typed_service.SendAsync(request);

                Console.WriteLine($"Hello, World! {i}");
            }            
        }
    }
}