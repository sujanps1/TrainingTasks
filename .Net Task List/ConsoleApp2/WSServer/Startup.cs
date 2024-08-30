using System.Net.WebSockets;
using System.Net;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System;

namespace WSServer
{
    public class Startup
    {

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            var wsOptions = new WebSocketOptions
            {
                KeepAliveInterval = TimeSpan.FromSeconds(120)
            };
            app.UseWebSockets(wsOptions);

            app.Use(async (context, next) =>
            {
                if (context.Request.Path == "/send" || context.Request.Path == "/login")
                {
                    if (context.WebSockets.IsWebSocketRequest)
                    {
                        using (WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync())
                        {
                            await HandleWebSocket(context, webSocket);
                        }
                    }
                    else
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    }
                }
                else
                {
                    await next();
                }
            });

            app.Use(async (context, next) =>
            {
                if (context.Request.Path == "/")
                {
                    context.Response.ContentType = "text/plain";
                    await context.Response.WriteAsync("Hi!!!! ,This  not a web application.");
                }
                else
                {
                    await next();
                }
            });
        }

        private async Task HandleWebSocket(HttpContext context, WebSocket webSocket)
        {
            var buffer = new byte[1024 * 4];
            WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), System.Threading.CancellationToken.None);

            while (result != null && !result.CloseStatus.HasValue)
            {
                string path = context.Request.Path;
                string msg = Encoding.UTF8.GetString(new ArraySegment<byte>(buffer, 0, result.Count));

                Console.WriteLine($"Received from client: {msg}");

                if (path == "/send")
                {
                    var serverMsg = Encoding.UTF8.GetBytes($"Server says: {DateTime.UtcNow:f}");
                    await webSocket.SendAsync(new ArraySegment<byte>(serverMsg), result.MessageType, result.EndOfMessage, System.Threading.CancellationToken.None);
                }
                else if (path == "/login")
                {
                    var serverMsg = Encoding.UTF8.GetBytes("Login successfully");
                    await webSocket.SendAsync(new ArraySegment<byte>(serverMsg), result.MessageType, result.EndOfMessage, System.Threading.CancellationToken.None);
                }

                result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), System.Threading.CancellationToken.None);
            }

            await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, System.Threading.CancellationToken.None);
        }
    }
}

