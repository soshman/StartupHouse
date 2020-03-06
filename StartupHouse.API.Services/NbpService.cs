using Newtonsoft.Json;
using StartupHouse.API.Interfaces;
using StartupHouse.API.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace StartupHouse.API.Services
{
    public class NbpService : INbpService
    {
        private readonly HttpClient _client;
        private readonly ICurrencyService _currencyService;

        public NbpService(HttpClient client,
            ICurrencyService currencyService)
        {
            _client = client;
            _currencyService = currencyService;
        }

        public async Task UpdateCurrencies(DateTime dateFrom, DateTime dateTo)
        {
            try
            {
                _client.DefaultRequestHeaders.Accept.Clear();
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await _client.GetAsync($"http://api.nbp.pl/api/exchangerates/tables/a/{dateFrom.ToString("yyyy-MM-dd")}/{dateTo.ToString("yyyy-MM-dd")}");

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var responseObject = JsonConvert.DeserializeObject<IEnumerable<NbpResponse>>(responseContent);
                    _currencyService.UpdateCurrencies(responseObject);
                }
                else if (response.StatusCode != HttpStatusCode.NotFound) //TODO: How to handle 404?
                {
                    throw new InvalidOperationException($"NBP Api returned http status: {response.StatusCode}.");
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
