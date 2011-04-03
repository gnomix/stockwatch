using solidware.financials.messages;

namespace solidware.financials.service.handlers
{
    public interface StockPriceLookupService
    {
        CurrentStockPrice FindPriceFor(string symbol);
    }
}