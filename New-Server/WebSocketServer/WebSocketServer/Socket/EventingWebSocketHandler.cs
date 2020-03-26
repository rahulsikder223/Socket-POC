using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Web.WebSockets;

namespace WebSocketServer.Socket
{
    public class EventingWebSocketHandler : WebSocketHandler
    {
        public override void OnOpen()
        {
            base.Send("Connected!");

            base.Send("Hello from here...");
        }
    }
}