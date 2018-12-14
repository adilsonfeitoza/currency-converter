using CurrencyConverter.Controllers;
using CurrencyConverter.Models;
using CurrencyConverter.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using Xunit;
using System.Linq;
using System.Collections;

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
            var currenciesResponse = Assert.IsType<CurrenciesResponse>(res.Value);
            Assert.IsType<IDictionary>(currenciesResponse.currencies);
        }
    }
}
