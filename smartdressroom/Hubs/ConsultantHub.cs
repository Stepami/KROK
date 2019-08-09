using Microsoft.AspNetCore.SignalR;
using smartdressroom.Services;
using System;
using System.Threading.Tasks;
using smartdressroom.HubModels;
using Newtonsoft.Json;

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
            if (consultantService.Rooms.Exists(r => r.HubID == Context.ConnectionId))
                consultantService.RemoveRoomAsync(Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }

        [HubMethodName("onRoomInitialized")]
        public async Task OnRoomInitialized() => await Clients.Caller.SendAsync("onRoomAdded", consultantService.AddRoom(Context.ConnectionId));

        [HubMethodName("onConsultantLoggedIn")]
        public async Task OnConsultantLoggedIn()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "consultants")
                .ContinueWith(t => Clients.Caller.SendAsync("onQueriesReceived", JsonConvert.SerializeObject(consultantService.Queries)));
        }

        [HubMethodName("onQueryMade")]
        public Task OnQueryMade(bool needsConsultant, Product product)
        {
            string id = consultantService.MakeQuery(needsConsultant, Context.ConnectionId, product);
            return Clients.Group("consultants").SendAsync("onQueriesReceived", JsonConvert.SerializeObject(consultantService.Queries))
                .ContinueWith(t => Task.Run(async () =>
                {
                    await Task.Delay(changingStatusTimeout * 1000);
                    consultantService.ChangeQueryStatusAsync(id);
                }));
        }

        [HubMethodName("onQuerySent")]
        public async Task OnQuerySent(string id, string servedBy) => await Clients.Caller.SendAsync("onQueryConfirmed", id, await consultantService.ConfirmQueryAsync(id, servedBy));

        [HubMethodName("onQueryClosed")]
        public void OnQueryClosed(string id, string servedBy) => consultantService.CloseQueryAsync(id, servedBy);

        [HubMethodName("onUpdateRequested")]
        public async Task OnUpdateRequested() => await Clients.Caller.SendAsync("onQueriesReceived", JsonConvert.SerializeObject(consultantService.Queries));
    }
}