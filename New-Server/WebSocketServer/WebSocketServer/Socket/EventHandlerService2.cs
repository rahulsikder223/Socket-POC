using Microsoft.Web.WebSockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace WebSocketServer.Socket
{
    public class EventHandlerService2 : IHttpHandler
    {
        static HttpListener httpListener = new HttpListener();

        public void Start()
        {
            httpListener.Prefixes.Add("http://localhost:8800/");
            httpListener.Start();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public void ProcessRequest(HttpContext context)
        {
            if (context.IsWebSocketRequest)
            {
                context.AcceptWebSocketRequest(new
                    EventingWebSocketHandler());
            }
        }
    }
}