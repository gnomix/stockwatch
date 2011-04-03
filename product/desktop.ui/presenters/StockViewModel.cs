using solidware.financials.windows.ui.views.controls;

namespace solidware.financials.windows.ui.presenters
{
    public class StockViewModel
    {
        public StockViewModel(string symbol)
        {
            Symbol = symbol;
            Price = Money.Null;
        }

        public string Symbol { get; set; }
        public Observable<Money> Price { get; set; }

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