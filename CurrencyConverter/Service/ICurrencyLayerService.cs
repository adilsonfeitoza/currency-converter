﻿using CurrencyConverter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyConverter.Service
{
    public interface ICurrencyLayerService
    {
        ResulteResponse GetListCurrencies();
        ConvertResponse Convert(string to, string from, decimal amount);
    }
}
