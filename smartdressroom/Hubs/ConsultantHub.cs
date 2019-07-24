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
        IConsultantService consultantService;

        public ConsultantHub(IConsultantService cs)
        {
            consultantService = cs;
        }

        public override Task OnConnectedAsync()
        {
            int roomNumber = consultantService.AddRoom(Context.ConnectionId);
            Clients.Caller.SendAsync("roomAdded", roomNumber);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            consultantService.RemoveRoom(Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }
    }
}
