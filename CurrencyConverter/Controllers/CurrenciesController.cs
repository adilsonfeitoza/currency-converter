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

        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            var currencies = _currencyLayerService.GetCurrencies();
            return Ok(currencies);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
