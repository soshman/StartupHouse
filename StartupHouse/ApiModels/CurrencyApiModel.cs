﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StartupHouse.API.ApiModels
{
    public class CurrencyApiModel
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
