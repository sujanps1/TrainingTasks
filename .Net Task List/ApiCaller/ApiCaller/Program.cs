using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApiCaller
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://jsonplaceholder.typicode.com");

            var apiService = new ApiService(httpClient);
            var apiUrl = "todos/1";

            var apiResponse = await apiService.GetApiDataAsync(apiUrl);

            Console.WriteLine($"API Response: {apiResponse}");
        }
    }

    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetApiDataAsync(string apiUrl)
        {
            return await _httpClient.GetStringAsync(apiUrl);
        }
    }
}