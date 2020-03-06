using System;
using System.Collections.Generic;

namespace StartupHouse.API.Interfaces.Models
{
    public class NbpResponse
    {
        public string Currency { get; set; }

        public string Code { get; set; }

        public IEnumerable<NbpCurrencyRate> Rates { get; set; }
    }
}
