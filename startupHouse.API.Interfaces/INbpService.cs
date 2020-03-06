using StartupHouse.API.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StartupHouse.API.Interfaces
{
    /// <summary>
    ///     Service that gets information from remote (NBP) API.
    /// </summary>
    public interface INbpService
    {
        /// <summary>
        ///     Method gets data from remote API about currency.
        /// </summary>
        /// <param name="code">Currency ISO 4217 code.</param>
        /// <param name="date">Date to get currency data about.</param>
        Task<NbpResponse> GetCurrency(string code, DateTime date);
    }
}
