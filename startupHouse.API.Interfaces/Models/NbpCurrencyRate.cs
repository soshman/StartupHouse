using System;

namespace StartupHouse.API.Interfaces.Models
{
    public class NbpCurrencyRate
    {
        public DateTime EffectiveDate { get; set; }

        public decimal Mid { get; set; }
    }
}
