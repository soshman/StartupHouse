using StartupHouse.API.Interfaces.DTO;
using StartupHouse.API.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StartupHouse.API.Interfaces
{
    /// <summary>
    ///     Service providing operations to handle currencies.
    /// </summary>
    public interface ICurrencyService
    {
        /// <summary>
        ///     Gets list of available currencies.
        /// </summary>
        IEnumerable<CurrencyDTO> GetAvailableCurrencies();

        /// <summary>
        ///     Gets currencies details.
        /// </summary>
        /// <param name="code">Currency ISO 4217 code.</param>
        /// <param name="dateFrom">Optional from date, if not set then results from today.</param>
        /// <param name="dateTo">Optional to date, if not set then results to today.</param>
        Task<CurrencyDetailsDTO> GetCurrencyDetails(string code, DateTime? dateFrom, DateTime? dateTo);

        /// <summary>
        ///     Method updates certain day currency data.
        /// </summary>
        /// <param name="code">Currency ISO 4217 code.</param>
        /// <param name="date">Date to update currency data.</param>
        Task UpdateCurrency(string code, DateTime date);

        /// <summary>
        ///     Method updates data about all currencies with today date.
        /// </summary>
        Task UpdateCurrencies();
    }
}
