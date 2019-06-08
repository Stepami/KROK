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
        EventHandler handler;

        public ConsultantHub(IQueryService qs)
        {
            handler = new EventHandler(async (sender, e) => await OnCountChanged());
            queryService = qs;
            queryService.CountChanged += handler;
        }

        public override Task OnConnectedAsync()
        {
            queryService.Connections++;
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            queryService.Connections--;
            return base.OnDisconnectedAsync(exception);
        }

        public Task OnCountChanged()
        {
            queryService.CountChanged -= handler;
            return Clients.All.SendAsync("countEvent", queryService.Connections);
        }
    }
}
