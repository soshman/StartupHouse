using Newtonsoft.Json;
using StartupHouse.API.Interfaces;
using StartupHouse.API.Interfaces.Exceptions;
using StartupHouse.API.Interfaces.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace StartupHouse.API.Services
{
    public class NbpService : INbpService
    {
        private readonly HttpClient _client;

        public NbpService(HttpClient client)
        {
            _client = client;
        }

        public async Task<NbpResponse> GetCurrency(string code, DateTime date)
        {
            try
            {
                _client.DefaultRequestHeaders.Accept.Clear();
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await _client.GetAsync($"http://api.nbp.pl/api/exchangerates/rates/a/{code}/{date.ToString("yyyy-MM-dd")}");

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var responseObject = JsonConvert.DeserializeObject<NbpResponse>(responseContent);
                    return responseObject;
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return null;
                }
                else
                {
                    throw new NbpApiException($"NBP Api returned http status: {response.StatusCode}.");
                }
            }
            catch (Exception ex)
            {
                throw new NbpApiException($"Something went wrong calling NBP API.", ex);
            }
        }
    }
}
