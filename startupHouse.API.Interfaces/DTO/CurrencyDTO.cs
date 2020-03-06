using System;
using System.Collections.Generic;
using System.Text;

namespace StartupHouse.API.Interfaces.DTO
{
    public class CurrencyDTO
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
    }
}
