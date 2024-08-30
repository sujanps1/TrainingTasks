using System.Text.Json;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using InterfaceReal;

public class LoginHandler : IHandler
{
    public async Task<object> HandleRequestAsync(HttpContext context)
    {
        var request = await JsonSerializer.DeserializeAsync<LoginRequest>(context.Request.Body);

        System.Console.WriteLine($"Received login request: UserID = {request.UserId}, Password = {request.Password}");

        await Task.Delay(500);

        return new LoginResponse
        {
            Success = true,
            Message = "Login successful!"
        };
    }
}
