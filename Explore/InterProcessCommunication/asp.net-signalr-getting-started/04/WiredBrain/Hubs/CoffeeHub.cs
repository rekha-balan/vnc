using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using WiredBrain.Helpers;

namespace WiredBrain.Hubs
{
    // Can use authorization

    //[Authorize]
    public class CoffeeHub: Hub
    {
        private readonly OrderChecker _orderChecker;

        public CoffeeHub(OrderChecker orderChecker)
        {
            _orderChecker = orderChecker;
        }

        public async Task GetUpdateForOrder(int orderId)
        {
            // Can access logged on user from Context property of Hub
            
            //this.Context.User
            CheckResult result;
            do
            {
                result = _orderChecker.GetUpdate(orderId);
                Thread.Sleep(500);
                if (result.New)
                    await Clients.Caller.SendAsync("ReceiveOrderUpdate", 
                        result.Update);
            } while (!result.Finished);
            await Clients.Caller.SendAsync("Finished");
        }

        //public override async Task OnConnectedAsync()
        //{
        //    // Examples.  Get ConnectionId.  Use with CLient
        //    // Everywhere in hub have access to Context.

        //    var connectionId = Context.ConnectionId;
        //    //    await Clients.Client(connectionId).SendAsync("NewOrder", order);

        //    //await Clients.AllExcept(connectionId).SendAsync("");

        //    // Property in Hub to add Clients to Groups

        //    //await Groups.AddToGroupAsync(connectionId, "CaffeineGroup");
        //    //await Groups.RemoveFromGroupAsync(connectionId, "CaffeineGroup");

        //    // Can use groups

        //    //await Clients.Group("CaffeineGroup").SendAsync("NewOrder", order);

        //    //return base.OnConnectedAsync();
        //}

        public override Task OnDisconnectedAsync(Exception exception)
        {


            return base.OnDisconnectedAsync(exception);
        }
    }
}
