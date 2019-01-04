using System;
using System.Net.Http;
using Microsoft.Owin.Hosting;

namespace MyFood.WebApi
{
    internal class Program
    {
        private static void Main()
        {
            const string apiAddress = "http://localhost:9000";
            using (WebApp.Start<Startup>(apiAddress))
            {
                var client = new HttpClient();
                var response = client.GetAsync(apiAddress + "/api/status").Result;
                Console.WriteLine($"Status: {response.Content.ReadAsStringAsync().Result} Status code: {response.StatusCode}");
                Console.ReadLine();
            }
        }
    }
}
