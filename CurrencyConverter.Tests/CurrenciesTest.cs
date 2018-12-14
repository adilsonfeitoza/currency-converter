using CurrencyConverter.Controllers;
using CurrencyConverter.Models;
using CurrencyConverter.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using Xunit;

namespace CurrencyConverter.Tests
{
    public class CurrenciesTest
    {
        private Mock<IConfiguration> configuration;
        private CurrencyLayerService CurrencyLayerService;

        public CurrenciesTest()
        {
            configuration = new Mock<IConfiguration>();
        }

        [Fact]
        public void ConvertOneCurrency()
        {
            configuration.Setup(r => r.GetValue<string>("Settings:apilayer")).Returns("http://apilayer.net/api");
            configuration.Setup(r => r.GetValue<string>("Settings:access_key")).Returns("e4e0deaf5a2fb6ef31b7ff2f608ef71f");
            CurrencyLayerService = new CurrencyLayerService(configuration.Object);
            var controller = new CurrenciesController(CurrencyLayerService);
            var result = controller.Get();

            var res = Assert.IsType<CreatedAtRouteResult>(result);
            Assert.Equal(201, res.StatusCode.Value);
            var currencies = Assert.IsType<CurrenciesResponse>(res.Value);
            //Assert.Contains("New User", currencies.FirstName);
        }
    }
}
