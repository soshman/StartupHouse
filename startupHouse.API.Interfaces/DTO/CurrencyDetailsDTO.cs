using System.Collections.Generic;

namespace StartupHouse.API.Interfaces.DTO
{
    public class CurrencyDetailsDTO
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
        ///     Currency avarage price from certain dates range.
        /// </summary>
        public decimal Average { get; set; }

        /// <summary>
        ///     Currency data from certain dates range.
        /// </summary>
        public IEnumerable<CurrencyPriceDTO> Prices { get; set; }
    }
}
