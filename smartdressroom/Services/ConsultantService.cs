using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using smartdressroom.HubModels;

namespace smartdressroom.Services
{
    public class ConsultantService : IConsultantService
    {
        private readonly IHubContext<Hubs.ConsultantHub> hubContext;

        public ConsultantService(IHubContext<Hubs.ConsultantHub> context) => hubContext = context;

        public List<string> Consultants { get; set; } = new List<string>();
        public List<Query> Queries { get; set; } = new List<Query>();
        public List<Room> Rooms { get; set; } = new List<Room>();
        
        public string MakeQuery(bool needsConsultant, string hub, Product product)
        {
            Room room = Rooms.Find(r => r.HubID == hub);
            Query q = new Query
            {
                Status = room.Responsible == null ? QueryStatus.FREE : QueryStatus.FREE_BUSY,
                Room = room,
                Product = product,
                NeedsConsultant = needsConsultant
            };
            Queries.Add(q);
            return q.ID;
        }

        public async void ChangeQueryStatusAsync(string id)
        {
            int i = Queries.IndexOf(Queries.Find(q => q.ID == id));
            if (i > -1)
            {
                if (Queries[i].Status == QueryStatus.FREE_BUSY && Queries[i].ServedBy == null)
                    Queries[i].Status = QueryStatus.FREE;
            }
            await hubContext.Clients.Group("consultants").SendAsync("onQueriesReceived", JsonConvert.SerializeObject(Queries));
        }

        public async void CloseQueryAsync(string id, string servedBy)
        {
            int i = Queries.IndexOf(Queries.Find(q => q.ID == id));
            if (i > -1)
            {
                if (Queries[i].ServedBy == servedBy)
                    Queries[i].Status = QueryStatus.CLOSED;
            }
            await hubContext.Clients.Group("consultants").SendAsync("onQueriesReceived", JsonConvert.SerializeObject(Queries));
        }

        public async Task<bool> ConfirmQueryAsync(string id, string servedBy)
        {
            bool confirmation = false;
            int i = Queries.IndexOf(Queries.Find(q => q.ID == id));
            if (i > -1)
            {
                Room room = Queries[i].Room;

                if (Queries[i].Status != QueryStatus.CLOSED &&
                    Queries[i].Status != QueryStatus.BUSY &&
                    Queries[i].ServedBy == null)
                {
                    if (room.Responsible == null)
                    {
                        room.Responsible = servedBy;
                        Rooms[room.Number - 1] = room;

                        Queries[i].ServedBy = servedBy;
                        Queries[i].Status = QueryStatus.BUSY;

                        for (int j = 0; j < Queries.Count; j++)
                        {
                            if (Queries[j].Status == QueryStatus.FREE &&
                            Queries[j].Room.Responsible == room.Responsible &&
                            Queries[j].Room.HubID == room.HubID)
                            {
                                if (Queries[j].ID != Queries[i].ID)
                                {
                                    Queries[j].Status = QueryStatus.FREE_BUSY;
                                }
                            }
                        }

                        confirmation = true;
                    }
                    else if (Queries[i].ServedBy == null)
                    {
                        Queries[i].ServedBy = servedBy;
                        Queries[i].Status = QueryStatus.BUSY;
                        confirmation = true;
                    }
                }
            }

            await hubContext.Clients.Group("consultants").SendAsync("onQueriesReceived", JsonConvert.SerializeObject(Queries));

            return confirmation;
        }

        public int AddRoom(string hub)
        {
            int i;
            if (Rooms.Count == 1)
            {
                if (Rooms[0].Number != 1)
                {
                    Rooms.Insert(0, new Room
                    {
                        HubID = hub,
                        Number = 1
                    });
                    return 1;
                }
            }
            for (i = 0; i < Rooms.Count - 1; i++)
            {
                if (i == 0)
                {
                    if (Rooms[i].Number != 1)
                    {
                        Rooms.Insert(i, new Room
                        {
                            HubID = hub,
                            Number = i + 1
                        });
                        return i + 1;
                    }
                }
                if (Rooms[i + 1].Number - Rooms[i].Number != 1)
                {
                    Rooms.Insert(i + 1, new Room
                    {
                        HubID = hub,
                        Number = i + 2
                    });
                    return i + 2;
                }
            }
            Rooms.Add(new Room
            {
                HubID = hub,
                Number = Rooms.Count + 1
            });
            return Rooms.Count;
        }

        public async void RemoveRoomAsync(string hub)
        {
            Rooms.Remove(Rooms.Find(r => r.HubID == hub));
            Queries.RemoveAll(q => q.Room.HubID == hub);
            await hubContext.Clients.Group("consultants").SendAsync("onQueriesReceived", JsonConvert.SerializeObject(Queries));
        }
    }
}