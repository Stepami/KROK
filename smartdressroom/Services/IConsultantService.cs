using smartdressroom.HubModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace smartdressroom.Services
{
    public interface IConsultantService
    {
        List<Query> Queries { get; set; }
        List<Room> Rooms { get; set; }
        string MakeQuery(bool needsConsultant, string hub, Product product);
        void ChangeQueryStatusAsync(string id);
        Task<bool> ConfirmQueryAsync(string id, string servedBy);
        void CloseQueryAsync(string id, string servedBy);
        int AddRoom(string hub);
        void RemoveRoom(string hub);
    }
}