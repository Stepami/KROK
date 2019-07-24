using smartdressroom.HubModels;
using System;
using System.Collections.Generic;

namespace smartdressroom.Services
{
    public interface IConsultantService
    {
        List<Query> Queries { get; set; }
        List<Room> Rooms { get; set; }
        void MakeQuery();
        void ChangeQueryStatus();
        void CloseQuery();
        int AddRoom(string hub);
        void RemoveRoom(string hub);
    }
}
