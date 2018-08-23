using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Threading.Tasks;

namespace SimpleChat
{
    // By default this will be projected in to Public API as ChatHub
    [HubName("chat")]
    public class ChatHub : Hub
    {
        public void SendMessage(string message)
        {
            var msg = String.Format("{0}: {1}", Context.ConnectionId, message);

            // All means all registered/subscribed connections
            Clients.All.newMessage(msg);

            // Others, everyone but me
            Clients.Others.newMessage(msg);
            // same as
            Clients.AllExcept(Context.ConnectionId);

            // All except
            Clients.AllExcept(new string[] {"ConnectionID1", "ConnectionID2" } );

            // Send back to caller
            Clients.Caller.newMessage(msg);
            // same as
            Clients.Client(Context.ConnectionId).newMessage(msg);
        }

        public void JoinRoom(string room)
        {
            // NOTE: this is not persisted - ....
            Groups.Add(Context.ConnectionId, room);
        }

        public void SendMessageToRoom(string room, string message)
        {
            var msg = String.Format("{0}: {1}", Context.ConnectionId, message);
            Clients.Group(room).newMessage(msg);
        }

        public void SendMessageData(SendData data)
        {
            // process incoming data...
            // transform data...
            // craft new data...

            Clients.All.newData(data);
        }

        //public Task<int> SendDataAsync()
        //{
        //    // async ... work...
        //}

        public override Task OnConnected()
        {
            SendMonitoringData("Connected", Context.ConnectionId);
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            SendMonitoringData("Disconnected", Context.ConnectionId);
            return base.OnDisconnected(stopCalled);
        }

        // OnDisconnected has a parameter now.  See infra.
        //public override Task OnDisconnected()
        //{
        //    SendMonitoringData("Disconnected", Context.ConnectionId);
        //    return base.OnDisconnected();
        //}

        public override Task OnReconnected()
        {
            SendMonitoringData("Reconnected", Context.ConnectionId);
            return base.OnReconnected();
        }

        private void SendMonitoringData(string eventType, string connection)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<MonitorHub>();
            context.Clients.All.newEvent(eventType, connection);
        }
    }

    public class SendData
    {
        public int Id { get; set; }
        public string Data { get; set; }
    }
}