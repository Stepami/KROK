using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using smartdressroom.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace smartdressroom.Hubs
{
    public class ConsultantHub : Hub
    {
        IQueryService queryService;

        public ConsultantHub(IQueryService qs) => queryService = qs;

        public override Task OnConnectedAsync()
        {
            queryService.Connections++;
            return Clients.All.SendAsync("countEvent", queryService.Connections);
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            queryService.Connections--;
            return Clients.All.SendAsync("countEvent", queryService.Connections);
        }
    }
}
