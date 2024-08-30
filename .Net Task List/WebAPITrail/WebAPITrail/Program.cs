using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApiCaller
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string apiUrl = "http://10.140.129.54/CashClubVanillaWebHost/CageWebService.svc/connect";

            var client = new HttpClient();

            var response = await client.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                Console.WriteLine("Response Content:");
                Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            }
            else
            {
                Console.WriteLine("Error: " + response.StatusCode);
            }
        }
    }
}