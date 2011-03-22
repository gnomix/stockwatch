using Machine.Specifications;
using solidware.financials.service.domain.accounting;

namespace specs.unit.service.domain.accounting
{
    public class CurrencySpecs
    {
        public abstract class concern : runner<Currency>
        {
            Cleanup clean = () => { 
                SimpleUnitOfMeasure.provide_rate((x, y) => ConversionRatio.Default);
            };
        }

        [Concern(typeof (Currency))]
        public class when_converting_one_USD_dollar_to_CAD : concern
        {
            Establish c = () =>
            {
                SimpleUnitOfMeasure.provide_rate((x, y) => new ConversionRatio(1.05690034));
            };

            It should_return_the_correct_amount = () =>
            {
                sut.convert(1, Currency.CAD).should_be_equal_to(1.05690034);
            };

            protected override Currency create_sut()
            {
                return Currency.USD;
            }
        }

        [Concern(typeof (Currency))]
        public class when_converting_one_CAD_dollar_to_USD : concern
        {
            Establish c = () =>
            {
                SimpleUnitOfMeasure.provide_rate((x, y) => new ConversionRatio(0.95057));
            };

            It should_return_the_correct_amount = () =>
            {
                sut.convert(1.05690034d, Currency.USD).should_be_equal_to(1.0046577561938002d);
            };

            protected override Currency create_sut()
            {
                return Currency.CAD;
            }
        }
    }
}