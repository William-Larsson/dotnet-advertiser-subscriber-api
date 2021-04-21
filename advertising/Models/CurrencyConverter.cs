using System;
using System.IO;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace advertising.Models
{
    public class CurrencyConverter
    {
        // Rapid API info for API calls. 
        public string APIKey = Environment.GetEnvironmentVariable("API_KEY");
        public string APIHost = Environment.GetEnvironmentVariable("API_HOST");
        public HttpClient client { get; set; }

        // Constructor, setup Http client with SSL certificate. 
        public CurrencyConverter(){
            // Bypass SSL certificate to get a proper SSL connection.
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            client = new HttpClient(clientHandler);
        }

        // TODO Return convertion rate 
        public async Task<double> GetExchangeRate(string from, string to)
        {
            var fromTo = $"{from}_{to}";
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{APIHost}?q={from}_{to}&compact=ultra&apiKey={APIKey}"),
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                string json = await response.Content.ReadAsStringAsync();
                var data = (JObject)JsonConvert.DeserializeObject(json);
                return (double)data[fromTo];
            }
        }
    }
}