using System;

namespace solidware.financials.service.handlers
{
    public class StubLookupService : StockPriceLookupService
    {
        public decimal FindPriceFor(string symbol)
        {
            return Convert.ToDecimal(new Random().NextDouble());
        }
    }
}