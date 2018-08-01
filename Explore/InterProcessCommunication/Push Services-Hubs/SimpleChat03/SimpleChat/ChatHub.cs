using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace SimpleChat
{
    // By default this will be projected in to Public API as ChatHub
    [HubName("chat")]
    public class ChatHub : Hub
    {
        //public void Hello()
        //{
        //    Clients.All.hello();
        //}

        public void SendMessage(string message)
        {
            Clients.All.hello();
        }
    }
}