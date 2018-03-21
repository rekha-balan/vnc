using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Microsoft.AspNet.SignalR;

namespace Chapter3.Code
{
    public class Chapter3Hub : Hub
    {
        public void BroadcastMessage(string text)
        {
            Clients.All.displayText(text);
        }
    }
}