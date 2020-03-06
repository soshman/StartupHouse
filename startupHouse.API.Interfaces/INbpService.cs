using StartupHouse.API.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StartupHouse.API.Interfaces
{
    public interface INbpService
    {
        Task<NbpResponse> GetCurrency(string code, DateTime date);
    }
}
