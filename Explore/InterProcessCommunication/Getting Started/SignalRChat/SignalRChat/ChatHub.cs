using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace SignalRChat
{
    public class ChatHub : Hub
    {
        // Declare public methods on the Hub so clients can call.
        //public void Hello()
        //{
        //    Clients.All.hello();
        //}
        public void Send(string name, string message)
        {
            // Call the broadcastMessage method to update clients.
            // broadcastMessage is declared in JavaScript in index.html
            // The Hub.Clients dynamic property provides access 
            // to all clients connected to hub

            Clients.All.broadcastMessage(name, message);      
        }
    }
}