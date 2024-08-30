using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace WSServer
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseCors();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapPost("/api/login", async context =>
                {
                    if (context.Request.ContentType != "application/json")
                    {
                        context.Response.StatusCode = StatusCodes.Status415UnsupportedMediaType;
                        await context.Response.WriteAsync("Unsupported Media Type");
                        return;
                    }

                    LoginRequest request;

                    try
                    {
                        request = await JsonSerializer.DeserializeAsync<LoginRequest>(context.Request.Body);
                    }
                    catch (JsonException e)
                    {
                        context.Response.StatusCode = StatusCodes.Status400BadRequest;
                        await context.Response.WriteAsync($"Invalid JSON: {e.Message}");
                        return;
                    }

                    // Log the received data
                    System.Console.WriteLine($"Received login request: UserID = {request.UserId}, Password = {request.Password}");

                    // Simulate a delay for processing
                    await Task.Delay(500);

                    // Send a response back to the client
                    var response = new LoginResponse
                    {
                        Success = true, // Change based on actual validation logic
                        Message = "Login successful!"
                    };

                    context.Response.ContentType = "application/json";
                    await JsonSerializer.SerializeAsync(context.Response.Body, response);
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
}
