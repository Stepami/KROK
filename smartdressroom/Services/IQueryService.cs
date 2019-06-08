using System;

namespace smartdressroom.Services
{
    public interface IQueryService
    {
        int Connections { get; set; }

        event EventHandler CountChanged;
    }
}
