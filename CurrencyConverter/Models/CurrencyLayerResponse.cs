using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyConverter.Models
{
    public class CurrenciesResponse
    {
        public IDictionary<string, string> currencies { get; set; }
    }

    public class QuotesResponse
    {
        public string source { get; set; }
        public IDictionary<string, decimal> quotes { get; set; }
    }
}
