using Machine.Specifications;
using solidware.financials.infrastructure;
using solidware.financials.messages;
using solidware.financials.service.handlers;

namespace specs.unit.service.handlers
{
    public class StockPriceRequestQueryHandlerSpecs
    {
        public abstract class concern
        {
            Establish context = () =>
            {
                bus = Create.dependency<ServiceBus>();
                service = Create.dependency<StockPriceLookupService>();
                sut = new StockPriceRequestQueryHandler(bus, service);
            };

            static protected StockPriceRequestQueryHandler sut;
            static protected ServiceBus bus;
            static protected StockPriceLookupService service;
        }

        public class when_looking_up_the_current_price_of_a_known_symbol : concern
        {
            Establish context = () =>
            {
                query = new StockPriceRequestQuery {Symbol = "ARX.TO"};
                service.is_told_to(x => x.FindPriceFor("ARX.TO")).it_will_return(26.81m);
            };

            Because of = () =>
            {
                sut.handle(query);
            };

            It should_publish_the_current_price = () =>
            {
                bus.was_told_to(x => x.publish(new CurrentStockPrice
                                               {
                                                   Symbol = "ARX.TO",
                                                   Price = 26.81m,
                                               }));
            };

            static StockPriceRequestQuery query;
        }
    }
}