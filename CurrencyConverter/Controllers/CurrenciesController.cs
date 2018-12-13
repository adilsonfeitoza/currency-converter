using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CurrencyConverter.Service;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyConverter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrenciesController : ControllerBase
    {
        private readonly ICurrencyLayerService _currencyLayerService;
        public CurrenciesController(ICurrencyLayerService currencyLayerService)
        {
            _currencyLayerService = currencyLayerService;
        }

        // GET api/Currencies
        [HttpGet]
        public IActionResult Get()
        {
            var currencies = _currencyLayerService.GetCurrencies();
            return new ObjectResult(currencies);
        }

        // GET api/Currencies/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "Currencies";
        }

        // POST api/Currencies
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/Currencies/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/Currencies/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
