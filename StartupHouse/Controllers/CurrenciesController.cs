using Microsoft.AspNetCore.Mvc;
using StartupHouse.API.ApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StartupHouse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrenciesController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<CurrencyApiModel> GetCurrencies ()
        {
            return null;
        }

        [Route("{code}")]
        [HttpGet]
        public CurrencyDetailsApiModel GetCurrencyData (string code, DateTime? fromDate, DateTime? toDate)
        {
            return null;
        }
    }
}
