using solidware.financials.infrastructure;
using solidware.financials.messages;

namespace solidware.financials.service.handlers
{
    public class StockPriceRequestQueryHandler : Handles<StockPriceRequestQuery>
    {
        ServiceBus bus;
        StockPriceLookupService service;

        public StockPriceRequestQueryHandler(ServiceBus bus, StockPriceLookupService service)
        {
            this.bus = bus;
            this.service = service;
        }

        public void handle(StockPriceRequestQuery item)
        {
            bus.publish(service.FindPriceFor(item.Symbol));
        }
    }
}