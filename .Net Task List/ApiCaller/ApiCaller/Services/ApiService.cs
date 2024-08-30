using System;
using System.Net.Http;
using System.Threading.Tasks;
using ApiCaller.Models;

namespace ApiCaller.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetApiDataAsync(string apiUrl)
        {
            using var response = await _httpClient.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
        }
    }
}