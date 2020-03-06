using System;

namespace StartupHouse.API.Interfaces.DTO
{
    public class CurrencyPriceDTO
    {
        public DateTime Day { get; set; }

        public decimal Price { get; set; }
    }
}
