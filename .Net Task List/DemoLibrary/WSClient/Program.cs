using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ConfigLibrary;

namespace WSClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Press enter to continue...");
            Console.ReadLine();

            var configReader = new ConfigReader();

            var cTs = new CancellationTokenSource();
            cTs.CancelAfter(TimeSpan.FromMinutes(20));

            while (true)
            {
                using (ClientWebSocket client = new ClientWebSocket())
                {
                    Console.WriteLine("Choose endpoint (/send or /login):");
                    string endpoint = Console.ReadLine();
                    var uri = $"ws://{configReader.ServerName}:{configReader.ServerPort}{endpoint}";
                    Uri serviceUri = new Uri(uri);

                    try
                    {
                        await client.ConnectAsync(serviceUri, cTs.Token);
                        Console.WriteLine($"Connected to the server at {endpoint}.");

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
                    catch (WebSocketException e)
                    {
                        Console.WriteLine($"WebSocket error: {e.Message}");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Unexpected error: {e.Message}");
                    }
                }

                Console.WriteLine("Do you want to continue? (y/n)");
                string continueOption = Console.ReadLine().ToLower();
                if (continueOption != "y")
                {
                    break;
                }
            }

            Console.WriteLine("Connection closed.");
        }
    }
}
