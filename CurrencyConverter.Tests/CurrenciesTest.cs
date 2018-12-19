using CurrencyConverter.Controllers;
using CurrencyConverter.Models;
using CurrencyConverter.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using Xunit;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System;

namespace CurrencyConverter.Tests
{
    public class CurrenciesTest
    {
        private IConfiguration mockConfiguration;
        private CurrencyLayerService CurrencyLayerService;

        public CurrenciesTest()
        {
            mockConfiguration = Substitute.For<IConfiguration>();
            mockConfiguration.GetValue<string>("Settings:apilayer").Returns("http://apilayer.net/api");
            mockConfiguration.GetValue<string>("Settings:access_key").Returns("e4e0deaf5a2fb6ef31b7ff2f608ef71f");
        }

        [Fact]
        public void ObterListaDeMoedas()
        {
            CurrencyLayerService = new CurrencyLayerService(mockConfiguration);
            var controller = new CurrenciesController(CurrencyLayerService);
            var result = controller.Get();

            var res = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, res.StatusCode);
            var currenciesResponse = Assert.IsType<ResulteResponse>(res.Value);
            Assert.NotEmpty(currenciesResponse.currencies);
        }

        [Fact]
        public void ConverterParaReal()
        {
            CurrencyLayerService = new CurrencyLayerService(mockConfiguration);
            decimal quote = CurrencyLayerService.GetListQuotes().quotes.FirstOrDefault(x => x.Key == $"USDBRL").Value;
            var controller = new CurrenciesController(CurrencyLayerService);
            var result = controller.Get("USD", quote);

            var res = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, res.StatusCode);
            var currenciesResponse = Assert.IsType<ConvertResponse>(res.Value);
            Assert.Equal(1, currenciesResponse.result);
        }


        [Fact]
        public void ConverterRealParaDolar()
        {
            CurrencyLayerService = new CurrencyLayerService(mockConfiguration);
            decimal quote = CurrencyLayerService.GetListQuotes().quotes.FirstOrDefault(x => x.Key == $"USDBRL").Value;
            var controller = new CurrenciesController(CurrencyLayerService);
            var result = controller.GetToBRL("USD", 1);

            var res = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, res.StatusCode);
            var currenciesResponse = Assert.IsType<ConvertResponse>(res.Value);
            Assert.Equal(Math.Round(quote), Math.Round(currenciesResponse.result));
        }
    }
}
