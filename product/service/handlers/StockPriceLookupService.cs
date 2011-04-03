namespace solidware.financials.service.handlers
{
    public interface StockPriceLookupService
    {
        decimal FindPriceFor(string symbol);
    }
}