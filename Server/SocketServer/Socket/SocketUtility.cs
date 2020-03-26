using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SuperSocket.SocketBase;
using SuperSocket.WebSocket;


namespace SocketServer.Socket
{
    public class SocketUtility
    {
        private static WebSocketServer _wsServer;
        private static SocketUtility _socketUtilityObject;

        public static SocketUtility GetInstance()
        {
            return _socketUtilityObject;
        }

        public static void CreateInstance()
        {
            if (_socketUtilityObject == null)
            {
                _socketUtilityObject = new SocketUtility();
            }
        }

        SocketUtility()
        {
            InitializeSocket();
        }

        public void InitializeSocket()
        {
            _wsServer = new WebSocketServer();
            int port = 8800;
            _wsServer.Setup(port);
            _wsServer.NewSessionConnected += WsServer_NewSessionConnected;
            _wsServer.NewDataReceived += WsServer_NewDataReceived;
            _wsServer.NewMessageReceived += WsServer_NewMessageReceived;
            _wsServer.SessionClosed += WsServer_SessionClosed;
            _wsServer.Start();
        }

        public void BroadcastMessage(string value)
        {
            if (value != null)
            {
                foreach (var s in _wsServer.GetAllSessions())
                {
                    s.Send(value);
                }
            }
        }

        private static void WsServer_SessionClosed(WebSocketSession session, CloseReason value)
        {
            Console.WriteLine("SessionClosed");
        }

        private static void WsServer_NewMessageReceived(WebSocketSession session, string value)
        {
            Console.WriteLine("NewMessageReceived: " + value);
            if (value != null)
            {
                session.Send("Hello from the other side...");
            }
        }

        private static void WsServer_NewDataReceived(WebSocketSession session, byte[] value)
        {
            Console.WriteLine("NewDataReceived");
        }

        private static void WsServer_NewSessionConnected(WebSocketSession session)
        {
            Console.WriteLine("NewSessionConnected");
        }
    }
}