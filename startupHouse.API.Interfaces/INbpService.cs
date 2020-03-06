using System;
using System.Threading.Tasks;

namespace StartupHouse.API.Interfaces
{
    public interface INbpService
    {
        Task UpdateCurrencies(DateTime dateFrom, DateTime dateTo);
    }
}
