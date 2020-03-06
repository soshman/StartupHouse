using StartupHouse.API.Interfaces.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartupHouse.API.Interfaces
{
    public interface ICurrencyService
    {
        IEnumerable<CurrencyDTO> GetAvailableCurrencies();
    }
}
