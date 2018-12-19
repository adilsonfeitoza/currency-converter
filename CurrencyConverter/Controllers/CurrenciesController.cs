using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CurrencyConverter.Service;

namespace CurrencyConverter.Controllers
{
    [Route("api/[controller]")]
    public class CurrenciesController : Controller
    {
        private readonly ICurrencyLayerService _currencyLayerService;
        public CurrenciesController(ICurrencyLayerService currencyLayerService)
        {
            _currencyLayerService = currencyLayerService;
        }

        // GET api/currencies
        [HttpGet]
        public IActionResult Get()
        {
            var currencies = _currencyLayerService.GetListCurrencies();
            return Ok(currencies);
        }

        // GET api/currencies/convert
        [HttpGet("convert/from-brl/")]
        public IActionResult Get([FromQuery(Name = "to")] string to, [FromQuery(Name = "amount")] decimal amount)
        {
            var result = _currencyLayerService.Convert("BRL", to, amount);
            return Ok(result);
        }


        // GET api/currencies/convert/tobrl
        [HttpGet("convert/to-brl/")]
        public IActionResult GetToBRL([FromQuery(Name = "from")] string from, [FromQuery(Name = "amount")] decimal amount)
        {
            var result = _currencyLayerService.Convert(from, "BRL", amount);
            return Ok(result);
        }
    }
}
