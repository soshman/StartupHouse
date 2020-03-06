using System;

namespace StartupHouse.API.Interfaces.Models
{
    public class NbpResponse
    {
        public string Currency { get; set; }

        public string Code { get; set; }

        public NbpCurrencyRate[] Rates { get; set; }
    }
}
