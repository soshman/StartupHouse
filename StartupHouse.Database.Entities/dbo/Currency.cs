using System.Collections.Generic;

namespace StartupHouse.Database.Entities.dbo
{
    /// <summary>
    ///     Currency entity.
    /// </summary>
    public class Currency
    {
        /// <summary>
        ///     Currency Id.
        /// </summary>
        public short Id { get; set; }

        /// <summary>
        ///     ISO 4217 currency code.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        ///     Currency name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Currency related prices.
        /// </summary>
        public virtual ICollection<CurrencyPrice> Prices { get; set; }
    }
}
