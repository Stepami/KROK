using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using smartdressroom.HubModels;

namespace smartdressroom.Services
{
    public class ConsultantService : IConsultantService
    {
        public List<Query> Queries { get; set; } = new List<Query>();
        public List<Room> Rooms { get; set; } = new List<Room>();
        
        public void MakeQuery()
        {
            throw new NotImplementedException();
        }

        public void ChangeQueryStatus()
        {
            throw new NotImplementedException();
        }

        public void CloseQuery()
        {
            throw new NotImplementedException();
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

        public void RemoveRoom(string hub)
        {
            Rooms.Remove(Rooms.Find(r => r.HubID == hub));
            Queries.RemoveAll(q => q.Room.HubID == hub);
        }
    }
}
