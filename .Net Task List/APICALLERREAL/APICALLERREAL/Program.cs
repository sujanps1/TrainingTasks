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
            httpClient.BaseAddress = new Uri("http://10.208.152.87:3003/cash-access/terminal/v1/NVGCAC50/login");

            //var apiUrl = "connect";
            var response = await httpClient.GetStringAsync(httpClient.BaseAddress);
            var apiResponse = new ApiResponse { Message = response };

            Console.WriteLine($"response: {apiResponse.Message}");
        }
    }

    public class ApiResponse
    {
        public string Message { get; set; }
    }
}




//    class Program
//    {
//        static async Task Main(string[] args)
//        {

//            var httpClient = new HttpClient();
//            httpClient.BaseAddress = new Uri("https://jsonplaceholder.typicode.com");

//            var apiService = new ApiService(httpClient);
//            var apiUrl = "todos/1";

//            var apiResponse = await apiService.GetApiDataAsync(apiUrl);

//            Console.WriteLine($"API Response: {apiResponse.Message}");
//        }
//    }

//    public class ApiService
//    {
//        private readonly HttpClient _httpClient;

//        public ApiService(HttpClient httpClient)
//        {
//            _httpClient = httpClient;
//        }

//        public async Task<ApiResponse> GetApiDataAsync(string apiUrl)
//        {
//            var response = await _httpClient.GetStringAsync(apiUrl);
//            return new ApiResponse { Message = response };
//        }
//    }

//    public class ApiResponse
//    {
//        public string Message { get; set; }
//    }
//}