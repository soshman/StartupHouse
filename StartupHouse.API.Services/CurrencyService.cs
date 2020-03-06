using AutoMapper;
using StartupHouse.API.Interfaces;
using StartupHouse.API.Interfaces.DTO;
using StartupHouse.API.Interfaces.Models;
using StartupHouse.Database.Entities.dbo;
using StartupHouse.Database.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public CurrencyDetailsDTO GetCurrencyDetails(string code, DateTime? dateFrom, DateTime? dateTo)
        {
            dateFrom = dateFrom ?? DateTime.Today;
            dateTo = dateTo ?? DateTime.Today;

            var currency = _currenciesRepository.Get(c => c.Code == code);

            var currencyDetailsDTO = _mapper.Map<CurrencyDetailsDTO>(currency);

            currencyDetailsDTO.Average = currency.Prices?
                .Where(cp => cp.Day >= dateFrom && cp.Day <= dateTo)
                .Select(p => p.Price)
                .Average() ?? 0M;

            return currencyDetailsDTO;
        }

        public async Task UpdateCurrencies(DateTime date)
        {
            await UpdateCurrencies(date, date);
        }

        public async Task UpdateCurrencies(DateTime dateFrom, DateTime dateTo)
        {
            var nbpResponse = await _nbpService.GetCurrencies(dateFrom, dateTo);

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
