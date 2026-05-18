using System;
using System.Collections.Generic;
using System.Text;

namespace TradingBot
{
    public class TradingBot
    {
        private readonly IExchangeService _exchange;

        public TradingBot(IExchangeService exchange)
        {
            _exchange = exchange;
        }

        public string ExecuteStrategy(string symbol, decimal averagePrice)
        {
            var currentPrice = _exchange.GetCurrentPrice(symbol);

            if (currentPrice <= averagePrice * 0.9m)
                return "Buy";

            if (currentPrice >= averagePrice * 1.1m)
                return "Sell";

            return "Hold";
        }
    }
}
