using System;
using System.Net.Http;

namespace subscribers.Models
{
    public class CurrencyConverter
    {
        // Rapid API info for API calls. 
        public string rapidAPIKey = Environment.GetEnvironmentVariable("RAPID_API_KEY");
        public string rapidAPIHost = Environment.GetEnvironmentVariable("RAPID_API_HOST");
        public HttpClient client { get; set; }

        // Constructor, setup Http client with SSL certificate. 
        public CurrencyConverter(){
            // Bypass SSL certificate to get a proper SSL connection.
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            client = new HttpClient(clientHandler);
        }

        // TODO Return convertion rate 
        public async void GetExchangeRate(string from, string to)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://alpha-vantage.p.rapidapi.com/query?to_currency={to}&function=CURRENCY_EXCHANGE_RATE&from_currency={from}"),
                Headers =
                {
                    { "x-rapidapi-key", rapidAPIKey },
                    { "x-rapidapi-host", rapidAPIHost },
                },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                Console.WriteLine(body);
            }
        }
    }
}