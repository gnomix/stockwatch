using solidware.financials.windows.ui.views.controls;

namespace solidware.financials.windows.ui.presenters
{
    public class StockViewModel
    {
        public string Symbol { get; set; }
        public Observable<decimal> Price { get; set; }

        public bool IsFor(string symbol)
        {
            return Symbol.Equals(symbol);
        }

        public void ChangePriceTo(decimal price)
        {
            Price.Value = price;
        }
    }
}