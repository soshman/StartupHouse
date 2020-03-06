using StartupHouse.API.Interfaces.DTO;
using StartupHouse.API.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StartupHouse.API.Interfaces
{
    public interface ICurrencyService
    {
        IEnumerable<CurrencyDTO> GetAvailableCurrencies();

        CurrencyDetailsDTO GetCurrencyDetails(string code, DateTime? dateFrom, DateTime? dateTo);

        Task UpdateCurrencies(DateTime date);

        Task UpdateCurrencies(DateTime dateFrom, DateTime dateTo);
    }
}
