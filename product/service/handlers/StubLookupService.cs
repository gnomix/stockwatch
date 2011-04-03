using System;
using solidware.financials.messages;

namespace solidware.financials.service.handlers
{
    public class StubLookupService : StockPriceLookupService
    {
        public CurrentStockPrice FindPriceFor(string symbol)
        {
            return new CurrentStockPrice
                   {
                       Symbol = symbol,
                       Price = Convert.ToDecimal(new Random().NextDouble()),
                   };
        }
    }
}