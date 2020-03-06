using System;

namespace StartupHouse.API.Interfaces.Models
{
    public class NbpCurrencyRate
    {
        public DateTime EffectiveDate { get; set; }

        public decimal Mid { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (!(obj is NbpCurrencyRate nbpCurrencyRate))
            {
                return false;
            }

            return (nbpCurrencyRate.EffectiveDate == EffectiveDate && nbpCurrencyRate.Mid == Mid);
        }

        public override int GetHashCode()
        {
            return Mid.GetHashCode() * 17 + EffectiveDate.GetHashCode();
        }
    }
}
