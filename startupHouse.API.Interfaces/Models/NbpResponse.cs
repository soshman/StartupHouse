using System;
using System.Collections.Generic;
using System.Linq;

namespace StartupHouse.API.Interfaces.Models
{
    public class NbpResponse
    {
        public string Currency { get; set; }

        public string Code { get; set; }

        public IEnumerable<NbpCurrencyRate> Rates { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (!(obj is NbpResponse nbpResponse))
            {
                return false;
            }

            if (!Enumerable.SequenceEqual(nbpResponse.Rates.OrderBy(r => r.EffectiveDate), Rates.OrderBy(r => r.EffectiveDate)))
                return false;

            return (nbpResponse.Currency == Currency && nbpResponse.Code == Code);
        }

        public override int GetHashCode()
        {
            return Currency.GetHashCode() * 17 + Code.GetHashCode() * 17 + Rates.GetHashCode() * 17;
        }
    }
}
