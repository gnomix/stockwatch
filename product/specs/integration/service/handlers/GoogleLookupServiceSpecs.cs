using Machine.Specifications;
using solidware.financials.service.handlers;

namespace specs.integration.service.handlers
{
    public class GoogleLookupServiceSpecs
    {
        public class when_looking_up_a_stock_price_from_google
        {
            Establish context = () =>
            {
                sut = new GoogleLookupService();
            };

            Because of = () =>
            {
                result = sut.FindPriceFor("ARX.TO");
            };

            It should_not_blow_up = () =>
            {
                result.should_be_greater_than(0m);
            };

            static GoogleLookupService sut;
            static decimal result;
        }
    }
}