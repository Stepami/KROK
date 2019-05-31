using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace smartdressroom.Hubs
{
    public class ConsoleHub : Hub
    {
        static int connections = 0;
        public override Task OnConnectedAsync()
        {
            connections += 1;
            return Clients.All.SendAsync("countEvent", connections.ToString());
        }
        public override Task OnDisconnectedAsync(Exception exception)
        {
            connections -= 1;
            return Clients.All.SendAsync("countEvent", connections.ToString());
        }
    }
}
