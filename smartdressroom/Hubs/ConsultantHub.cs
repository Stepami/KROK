using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace smartdressroom.Hubs
{
    public class ConsultantHub : Hub
    {
        static List<string> IDs = new List<string>();
        public override Task OnConnectedAsync()
        {
            IDs.Add(Guid.NewGuid().ToString());
            return Clients.All.SendAsync("countEvent", JsonConvert.SerializeObject(IDs, Formatting.Indented));
        }
        public override Task OnDisconnectedAsync(Exception exception)
        {
            IDs.RemoveAt(IDs.Count - 1);
            return Clients.All.SendAsync("countEvent", JsonConvert.SerializeObject(IDs, Formatting.Indented));
        }
    }
}
