using System;
using System.Collections.Generic;
using System.Text;

namespace StartupHouse.API.Interfaces.DTO
{
    public class CurrencyPriceDTO
    {
        public DateTime Day { get; set; }

        public decimal Price { get; set; }
    }
}
