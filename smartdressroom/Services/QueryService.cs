using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace smartdressroom.Services
{
    public class QueryService : IQueryService
    {
        private int count = 0;
        public int Connections
        {
            get => count;
            set
            {
                count = value;
                EventHandler @event = CountChanged;
                @event?.Invoke(this, EventArgs.Empty);
            }
        }

        public event EventHandler CountChanged;
    }
}
