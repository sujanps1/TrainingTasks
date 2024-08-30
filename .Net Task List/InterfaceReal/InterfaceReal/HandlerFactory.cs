using InterfaceReal;
using Microsoft.AspNetCore.Http;
using System;
using System.Text.Json;
using System.Threading.Tasks;
public class HandlerFactory
{
    public async Task HandleEndpointRequestAsync(HttpContext context, string endpoint)
    {
        IHandler handler = endpoint switch
        {
            "/api/login" => new LoginHandler(),
            "/api/select-language" => new LanguageSelectionHandler(),
            "/api/card" => new CardHandler(),  // Added CardHandler
            _ => throw new ArgumentException("Invalid endpoint")
        };

        var response = await handler.HandleRequestAsync(context);

        context.Response.ContentType = "application/json";
        await JsonSerializer.SerializeAsync(context.Response.Body, response);
    }
}

