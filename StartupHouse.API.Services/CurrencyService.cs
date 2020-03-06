using AutoMapper;
using StartupHouse.API.Interfaces;
using StartupHouse.API.Interfaces.DTO;
using StartupHouse.Database.Entities.dbo;
using StartupHouse.Database.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartupHouse.API.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly IMapper _mapper;
        
        private readonly IRepository<Currency> _currenciesRepository;
        
        public CurrencyService(IMapper mapper,
            IRepository<Currency> currenciesRepository)
        {
            _mapper = mapper;
            _currenciesRepository = currenciesRepository;
        }

        public IEnumerable<CurrencyDTO> GetAvailableCurrencies()
        {
            var currencies = _currenciesRepository.Fetch();

            return _mapper.Map<IEnumerable<CurrencyDTO>>(currencies);
        }
    }
}
