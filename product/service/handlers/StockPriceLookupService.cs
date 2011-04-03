namespace solidware.financials.service.handlers
{
    public interface StockPriceLookupService
    {
        decimal FindPriceFor(string symbol);
    }

    public class StubLookupService : StockPriceLookupService
    {
        public decimal FindPriceFor(string symbol)
        {
            return 24.00m;
        }
    }
}