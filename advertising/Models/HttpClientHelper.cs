using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

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


        // Fetches the subscriber of the given id from the subscriber API. 
        public async Task<CreateAdViewModel> GetSubscriber (long id) 
        {
            var response = await HttpClient.GetAsync($"/api/Subscriber/{id}");
            Advertiser result = null;

            if (response.IsSuccessStatusCode) 
            {
                string json = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<Advertiser>(json);
                return new CreateAdViewModel ()
                {
                    PersonalId = (long) result.PersonalId,
                    Firstname = result.Firstname,
                    Lastname = result.Lastname,
                    PhoneNumber = result.PhoneNumber,
                    DistributionAddress = result.DistributionAddress,
                    ZipCode = result.ZipCode,
                    City = result.City,
                    isOrganization = result.isOrganization,
                    SubscriberId = result.SubscriberId
                };
            }
            else return null;
        } 

        // Update a given subscriber with the API.
        public async void UpdateSubscriber (CreateAdViewModel vm)
        {
            var jsonContent = new StringContent(JsonConvert.SerializeObject(
                new Subscriber ()
                {
                    SubscriberId = (long) vm.SubscriberId,
                    PersonalId = vm.PersonalId,
                    Firstname = vm.Firstname,
                    Lastname = vm.Lastname,
                    DistributionAddress = vm.DistributionAddress,
                    ZipCode = vm.ZipCode,
                    City = vm.City,
                    PhoneNumber = vm.PhoneNumber
                }),
                Encoding.UTF8,
                "application/json"
            );

            HttpResponseMessage response = await HttpClient
                .PutAsync($"/api/Subscriber/{vm.SubscriberId}", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
            } 
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Failed to call the Subscriber Api: {response.StatusCode}");
                string content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Content: {content}");
                Console.ResetColor();
            }

            // TODO: Return the result in some way?
        }
    }
}