using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Press enter to connect to the WebSocket server...");
        Console.ReadLine();

        using (ClientWebSocket client = new ClientWebSocket())
        {
            Uri serviceUri = new Uri("ws://localhost:5022/ws");
            var cTs = new CancellationTokenSource();
            cTs.CancelAfter(TimeSpan.FromSeconds(180));

            try
            {
                await client.ConnectAsync(serviceUri, cTs.Token);
                Console.WriteLine("Connected to the server.");

                while (client.State == WebSocketState.Open)
                {
                    Console.WriteLine("Enter message to send:");
                    string message = Console.ReadLine();

                    if (!string.IsNullOrEmpty(message))
                    {
                        var byteToSend = new ArraySegment<byte>(Encoding.UTF8.GetBytes(message));
                        await client.SendAsync(byteToSend, WebSocketMessageType.Text, true, cTs.Token);

                        var responseBuffer = new byte[1024];
                        var response = await client.ReceiveAsync(new ArraySegment<byte>(responseBuffer), cTs.Token);
                        var responseMessage = Encoding.UTF8.GetString(responseBuffer, 0, response.Count);

                        Console.WriteLine($"Server says: {responseMessage}");
                    }
                }
            }
            catch (WebSocketException e)
            {
                Console.WriteLine($"WebSocket error: {e.Message}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unexpected error: {e.Message}");
            }
        }

        Console.WriteLine("Connection closed.");
    }
}
