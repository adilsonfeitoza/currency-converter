using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Linq.Expressions;
using CurrencyConverter.Service;
using CurrencyConverter.Models;

namespace CurrencyConverter.Service
{
    public class CurrencyLayerService : ICurrencyLayerService
    {
        private IConfiguration configuration;
        private string URL_API_CURRENCY_LAYER;
        private string API_ACCESS_KEY;

        public CurrencyLayerService(IConfiguration iConfig)
        {
            configuration = iConfig;
            URL_API_CURRENCY_LAYER = configuration.GetValue<string>("Settings:apilayer");
            API_ACCESS_KEY = configuration.GetValue<string>("Settings:access_key");
        }

        public CurrenciesResponse GetCurrencies()
        {
           return this.Get<CurrenciesResponse>($"{ URL_API_CURRENCY_LAYER }/list?access_key={ API_ACCESS_KEY }");
        }

        private T Get<T>(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                Task<HttpResponseMessage> response = client.GetAsync(url);
                response.Wait();

                Task<string> responseString = response.Result.Content.ReadAsStringAsync();
                responseString.Wait();

                return JsonConvert.DeserializeObject<T>(responseString.Result);
            }
        }

    }
}
