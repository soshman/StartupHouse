using System;

namespace StartupHouse.API.Interfaces.Models
{
    public class NbpResponse
    {
        public DateTime EffectiveDate { get; set; }
        public NbpCurrencyRate[] Rates { get; set; }
    }
}
