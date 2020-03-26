using System;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebSocketServer.Socket
{
    public class EventHandlerService
    {
        static HttpListener httpListener = new HttpListener();
        private static Mutex signal = new Mutex();
        public static async void Start()
        {
            httpListener.Prefixes.Add("http://localhost:8800/");
            httpListener.Start();
            while (signal.WaitOne())
            {
                await ReceiveConnection();
                break;
            }

        }

        public static async Task ReceiveConnection()
        {
            Console.WriteLine("Connection Established...");
            signal.ReleaseMutex();
        }

        public async static void BroadcastMessage(string message)
        {
            HttpListenerContext context = await httpListener.GetContextAsync();
            if (context.Request.IsWebSocketRequest)
            {
                HttpListenerWebSocketContext webSocketContext = await context.AcceptWebSocketAsync(null);
                WebSocket webSocket = webSocketContext.WebSocket;
                if (webSocket.State == WebSocketState.Open)
                {
                    await webSocket.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(message)),
                        WebSocketMessageType.Text, true, CancellationToken.None);
                }
            }
            //signal.ReleaseMutex();
        }
    }
}