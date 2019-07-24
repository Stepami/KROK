using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace smartdressroom.HubModels
{
    public class Room
    {
        public int Number { get; set; }
        public string Responsible { get; set; } = null;
        public bool NeedsConsultant { get; set; } = true;
        public string HubID { get; set; }
    }
}