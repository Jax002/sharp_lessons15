using System;
using System.Collections.Generic;
using System.Text;

namespace TradingBot
{
    public interface IExchangeService
    {
        decimal GetCurrentPrice(string symbol);
    }
}
