using gorilla.utility;
using solidware.financials.infrastructure.eventing;

namespace solidware.financials.messages
{
    public class StartWatchingSymbol : ValueType<StartWatchingSymbol>, Event
    {
        public string Symbol { get; set; }

        public override string ToString()
        {
            return "I will start watching {0}".format(Symbol);
        }
    }
}