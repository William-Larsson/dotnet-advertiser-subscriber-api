using System;
using System.Net.Http;

namespace advertising.Models
{
    // Helper class for setting up a Http connection to the API.
    public class HttpClientHelper
    {
        public HttpClient HttpClient { get; set; }

        public HttpClientHelper ()
        {
            // Bypass SSL certificate to get a proper SSL connection.
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            HttpClient = new HttpClient(clientHandler);
            HttpClient.BaseAddress = new Uri("https://localhost:5001");
        }
    }
}