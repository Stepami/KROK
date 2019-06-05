using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace smartdressroom.Services
{
    public class QueryService : IQueryService
    {
        public int Connections { get; set; } = 0;
    }
}
