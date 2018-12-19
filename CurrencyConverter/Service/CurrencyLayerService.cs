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

        public ResulteResponse GetListCurrencies()
        {
           return this.Get<ResulteResponse>($"{ URL_API_CURRENCY_LAYER }/list?access_key={ API_ACCESS_KEY }");
        }

        public QuotesResponse GetListQuotes()
        {
            return this.Get<QuotesResponse>($"{ URL_API_CURRENCY_LAYER }/live?access_key={ API_ACCESS_KEY }");
        }
        

        public ConvertResponse Convert(string to, string from, decimal amount)
        {
            decimal result = 0;
            var listQuotes = this.GetListQuotes();
            decimal quoteTo = listQuotes.quotes.FirstOrDefault(x => x.Key == $"USD{to.ToUpper()}").Value;
            decimal quoteFrom = listQuotes.quotes.FirstOrDefault(x => x.Key == $"USD{from.ToUpper()}").Value;

            if (quoteTo == 0 || quoteFrom == 0)            
                return new ConvertResponse(result);
            
            result = amount / (quoteTo / quoteFrom);
            return new ConvertResponse(result);
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
