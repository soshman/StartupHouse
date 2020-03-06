using AutoMapper;
using StartupHouse.API.Interfaces;
using StartupHouse.API.Interfaces.DTO;
using StartupHouse.Database.Entities.dbo;
using StartupHouse.Database.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StartupHouse.API.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly IMapper _mapper;
        private readonly IContextScope _contextScope;
        private readonly IRepository<Currency> _currenciesRepository;
        private readonly IRepository<CurrencyPrice> _currencyPricesRepository;
        private readonly INbpService _nbpService;

        public CurrencyService(IMapper mapper,
            IContextScope contextScope,
            IRepository<Currency> currenciesRepository,
            IRepository<CurrencyPrice> currencyPricesRepository,
            INbpService nbpService)
        {
            _mapper = mapper;
            _contextScope = contextScope;
            _currenciesRepository = currenciesRepository;
            _currencyPricesRepository = currencyPricesRepository;
            _nbpService = nbpService;
        }

        public IEnumerable<CurrencyDTO> GetAvailableCurrencies()
        {
            var currencies = _currenciesRepository.Fetch();

            return _mapper.Map<IEnumerable<CurrencyDTO>>(currencies);
        }

        public async Task<CurrencyDetailsDTO> GetCurrencyDetails(string code, DateTime? dateFrom, DateTime? dateTo)
        {
            dateFrom = dateFrom ?? DateTime.Today;
            dateTo = dateTo ?? DateTime.Today;

            if (dateFrom > DateTime.Today)
                dateFrom = DateTime.Today;

            if (dateTo > DateTime.Today)
                dateTo = DateTime.Today;

            //TODO: Check if range not too wide and if date from not > date to.

            var currency = _currenciesRepository.Get(c => c.Code == code);
            //TODO: If not found.

            var currencyPricesDates = _currencyPricesRepository.Query().Where(cp => cp.CurrencyId == currency.Id && cp.Day >= dateFrom && cp.Day <= dateTo).Select(cp => cp.Day).ToList();

            for (int i = 0; i < dateTo.Value.Subtract(dateFrom.Value).Days; i++)
            {
                var day = dateFrom.Value.AddDays(i);
                var currencyDayPrice = currencyPricesDates.FirstOrDefault(cpd => cpd == day);

                if (currencyDayPrice == default)
                    await UpdateCurrencies(day); //Could update only one currency.
            }

            var currencyPrices = _currencyPricesRepository.Fetch(cp => cp.CurrencyId == currency.Id && cp.Day >= dateFrom && cp.Day <= dateTo);

            var currencyDetailsDTO = _mapper.Map<CurrencyDetailsDTO>(currency);
            currencyDetailsDTO.Prices = _mapper.Map<IEnumerable<CurrencyPriceDTO>>(currencyPrices);

            var prices = currencyPrices.Select(p => p.Price);

            if (prices.Any())
                currencyDetailsDTO.Average = prices.Average();

            return currencyDetailsDTO;
        }

        public async Task UpdateCurrencies(DateTime date)
        {
            await UpdateCurrencies(date, date);
        }

        public async Task UpdateCurrencies(DateTime dateFrom, DateTime dateTo)
        {
            var nbpResponse = await _nbpService.GetCurrencies(dateFrom, dateTo);

            if (nbpResponse == null)
                return;

            foreach (var day in nbpResponse)
            {
                foreach (var currencyRate in day.Rates)
                {
                    var currency = _currenciesRepository.Get(c => c.Code == currencyRate.Code); //TODO: Maybe get a whole list at once? 

                    if (currency == null)
                    {
                        currency = new Currency
                        {
                            Code = currencyRate.Code,
                            Name = currencyRate.Currency
                        };
                        _currenciesRepository.Add(currency);
                    }

                    if (_currencyPricesRepository.Query().Any(cp => cp.CurrencyId == currency.Id && cp.Day == day.EffectiveDate))
                        continue;

                    var currencyPrice = new CurrencyPrice
                    {
                        Day = day.EffectiveDate,
                        Price = currencyRate.Mid,
                        Currency = currency
                    };

                    _currencyPricesRepository.Add(currencyPrice);
                }
            }

            _contextScope.Commit();
        }
    }
}
