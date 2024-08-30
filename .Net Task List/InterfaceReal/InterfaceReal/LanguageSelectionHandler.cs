using System.Text.Json;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using InterfaceReal;

public class LanguageSelectionHandler : IHandler
{
    public async Task<object> HandleRequestAsync(HttpContext context)
    {
        var request = await JsonSerializer.DeserializeAsync<LanguageSelectionRequest>(context.Request.Body);
        var selectedLanguage = request.Language;

        System.Console.WriteLine($"Received language selection: {selectedLanguage}");

        await Task.Delay(500);

        return new LanguageSelectionResponse
        {
            Success = true,
            Message = $"Language '{selectedLanguage}' selected successfully!"
        };
    }
}
