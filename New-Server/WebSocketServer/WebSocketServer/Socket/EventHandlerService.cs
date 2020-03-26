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
        static AutoResetEvent signal = new AutoResetEvent(true);
        static WebSocket webSocket;

        public async static void Start()
        {
            httpListener.Prefixes.Add("http://localhost:8800/");
            httpListener.Start();

            while (signal.WaitOne())
            {
                HttpListenerContext context = await httpListener.GetContextAsync();
                if (context.Request.IsWebSocketRequest)
                {
                    HttpListenerWebSocketContext webSocketContext = await context.AcceptWebSocketAsync(null);
                    webSocket = webSocketContext.WebSocket;
                }
                signal.Set();
            }
        }

        public async static void BroadcastMessage(string message)
        {
            if (webSocket != null && webSocket.State == WebSocketState.Open)
            {
                await webSocket.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(message)),
                    WebSocketMessageType.Text, true, CancellationToken.None);
            }
        }
    }
}