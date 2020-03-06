using StartupHouse.API.Interfaces.DTO;
using StartupHouse.API.Interfaces.Models;
using System.Collections.Generic;

namespace StartupHouse.API.Interfaces
{
    public interface ICurrencyService
    {
        IEnumerable<CurrencyDTO> GetAvailableCurrencies();

        CurrencyDetailsDTO GetCurrencyDetails(string code);

        void UpdateCurrencies(IEnumerable<NbpResponse> nbpResponse);
    }
}
