using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StartupHouse.API.ApiModels;
using StartupHouse.API.Interfaces;
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
        private readonly IMapper _mapper;

        private readonly ICurrencyService _currencyService;

        public CurrenciesController(IMapper mapper,
            ICurrencyService currencyService)
        {
            _mapper = mapper;
            _currencyService = currencyService;
        }

        [HttpGet]
        public IEnumerable<CurrencyApiModel> GetCurrencies()
        {
            var currenciesDto = _currencyService.GetAvailableCurrencies();

            return _mapper.Map<IEnumerable<CurrencyApiModel>>(currenciesDto);
        }

        [Route("{code}")]
        [HttpGet]
        public CurrencyDetailsApiModel GetCurrencyData(string code, DateTime? fromDate, DateTime? toDate)
        {
            var currencyDetailsDto = _currencyService.GetCurrencyDetails(code, fromDate, toDate);

            return _mapper.Map<CurrencyDetailsApiModel>(currencyDetailsDto);
        }
    }
}
