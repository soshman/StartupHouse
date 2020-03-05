using System;

namespace StartupHouse.Database.Entities.dbo
{
    /// <summary>
    ///     Currency price entity that contains info about price of the currency on the certain day.
    /// </summary>
    public class CurrencyPrice
    {
        /// <summary>
        ///     Currency id.
        /// </summary>
        public short CurrencyId { get; set; }
        
        /// <summary>
        ///     The day currency had certain price.
        /// </summary>
        public DateTime Day { get; set; }

        /// <summary>
        ///     Currency Price.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        ///     Currency navigation property.
        /// </summary>
        public Currency Currency { get; set; }
    }
}
