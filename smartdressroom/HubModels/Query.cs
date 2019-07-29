using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace smartdressroom.HubModels
{
    public class Query
    {
        public string ID { get; set; }
        public DateTime CreatedAt { get; set; }
        public QueryStatus Status { get; set; }
        public string ServedBy { get; set; } = null;
        public Room Room { get; set; }
        public Product Product { get; set; }
    }
}
