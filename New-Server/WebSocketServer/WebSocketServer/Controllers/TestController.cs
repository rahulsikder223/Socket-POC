using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebSocketServer.Socket;

namespace WebSocketServer.Controllers
{
    public class TestController : ApiController
    {
        [HttpPost]
        [ActionName("Trigger")]
        public bool Trigger([FromBody] string message)
        {
            EventHandlerService.BroadcastMessage(message);
            return true;
        }
    }
}
