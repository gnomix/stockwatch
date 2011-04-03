using gorilla.utility;
using solidware.financials.infrastructure.eventing;

namespace solidware.financials.messages
{
    public class CurrentStockPrice : ValueType<CurrentStockPrice>, Event
    {
        public string Symbol { get; set; }
        public decimal Price { get; set; }

        public override string ToString()
        {
            return "{0}: {1:C}".format(Symbol, Price);
        }
    }
}