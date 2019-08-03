using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using smartdressroom.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using smartdressroom.HubModels;
using System.Threading;

namespace smartdressroom.Hubs
{
    public class ConsultantHub : Hub
    {
        private readonly IConsultantService consultantService;
        private const int changingStatusTimeout = 60;

        public ConsultantHub(IConsultantService cs) => consultantService = cs;

        public override Task OnConnectedAsync() => base.OnConnectedAsync();

        public override Task OnDisconnectedAsync(Exception exception)
        {
            if (consultantService.Rooms.Find(r => r.HubID == Context.ConnectionId) != null)
                consultantService.RemoveRoom(Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }

        public async Task OnRoomInitialized() => await Clients.Caller.SendAsync("roomAdded", consultantService.AddRoom(Context.ConnectionId));

        public Task OnQueryMade(bool needsConsultant, Product product)
        {
            string id = consultantService.MakeQuery(needsConsultant, Context.ConnectionId, product);
            return Clients.All.SendAsync("queryAdded", consultantService.Queries.Find(q => q.ID == id))
                .ContinueWith(t => Task.Run(async () =>
                {
                    await Task.Delay(changingStatusTimeout * 1000);
                    consultantService.ChangeQueryStatusAsync(id);
                }));
        }
    }
}
