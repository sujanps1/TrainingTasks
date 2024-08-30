using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();

        app.UseAuthorization();

        var handlerFactory = new HandlerFactory();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();

            endpoints.MapPost("/api/login", async context =>
            {
                await handlerFactory.HandleEndpointRequestAsync(context, "/api/login");
            });

            endpoints.MapPost("/api/select-language", async context =>
            {
                await handlerFactory.HandleEndpointRequestAsync(context, "/api/select-language");
            });

            endpoints.MapPost("/api/card", async context =>
            {
                await handlerFactory.HandleEndpointRequestAsync(context, "/api/card");
            });
        });
    }
}

public class LoginRequest
{
    public string UserId { get; set; }
    public string Password { get; set; }
}

public class LoginResponse
{
    public bool Success { get; set; }
    public string Message { get; set; }
}

public class LanguageSelectionRequest
{
    public string Language { get; set; }
}

public class LanguageSelectionResponse
{
    public bool Success { get; set; }
    public string Message { get; set; }
}
