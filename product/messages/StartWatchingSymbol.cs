using gorilla.utility;

namespace solidware.financials.messages
{
    public class StartWatchingSymbol : ValueType<StartWatchingSymbol>
    {
        public string Symbol { get; set; }
    }
}