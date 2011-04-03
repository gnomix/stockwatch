using gorilla.utility;

namespace solidware.financials.messages
{
    public class StockPriceRequestQuery : ValueType<StockPriceRequestQuery>
    {
        public string Symbol { get; set; }
    }
}