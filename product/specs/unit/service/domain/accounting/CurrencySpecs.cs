using Machine.Specifications;
using solidware.financials.service.domain.accounting;

namespace specs.unit.service.domain.accounting
{
    public class CurrencySpecs
    {
        public abstract class concern 
        {
            Cleanup clean = () =>
            {
                SimpleUnitOfMeasure.provide_rate((x, y) => ConversionRatio.Default);
            };
        }

        [Concern(typeof (Currency))]
        public class when_converting_one_USD_dollar_to_CAD : concern
        {
            Establish c = () =>
            {
                SimpleUnitOfMeasure.provide_rate((x, y) => new ConversionRatio(1.05690034m));
            };

            It should_return_the_correct_amount = () =>
            {
                Currency.USD.convert(1, Currency.CAD).should_be_equal_to(1.05690034m);
            };
        }

        [Concern(typeof (Currency))]
        public class when_converting_one_CAD_dollar_to_USD : concern
        {
            Establish c = () =>
            {
                SimpleUnitOfMeasure.provide_rate((x, y) => new ConversionRatio(0.95057m));
            };

            It should_return_the_correct_amount = () =>
            {
                Currency.CAD.convert(1.05690034m, Currency.USD).should_be_equal_to(1.0046577561938002m);
            };
        }
    }
}