using System;
using System.Collections.Generic;
using System.Text;

namespace StartupHouse.API.ApiModels
{
    public class CurrencyPriceApiModel
    {
        public DateTime Day { get; set; }

        public decimal Price { get; set; }
    }
}
