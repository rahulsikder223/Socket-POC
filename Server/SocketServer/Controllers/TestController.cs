using SocketServer.Socket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SocketServer.Controllers
{
    public class TestController : ApiController
    {
        [HttpPost]
        [ActionName("Trigger")]
        public bool TestMethod([FromBody] string message)
        {
            SocketUtility socketObject = SocketUtility.GetInstance();
            socketObject.BroadcastMessage(message);
            return true;
        }
    }
}
