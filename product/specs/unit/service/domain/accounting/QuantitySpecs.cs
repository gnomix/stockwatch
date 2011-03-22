using Machine.Specifications;
using solidware.financials.service.domain.accounting;

namespace specs.unit.service.domain.accounting
{
    public class QuantitySpecs
    {
        public abstract class concern
        {
            Establish blah = () =>
            {
                sut = new Quantity(100, Currency.CAD);
            };

            static protected Quantity sut;
        }

        [Concern(typeof (Quantity))]
        public class when_checking_if_two_quantities_are_equal : concern
        {
            It should_return_true_when_they_represent_the_same_amount_and_units = () =>
            {
                sut.should_be_equal_to(new Quantity(100, Currency.CAD));
            };

            It should_return_false_when_they_are_not_the_same_amount = () =>
            {
                sut.ShouldNotEqual(new Quantity(1, Currency.CAD));
            };

            It should_return_false_when_they_represent_different_currencies = () =>
            {
                sut.ShouldNotEqual(new Quantity(100, Currency.USD));
            };
        }
    }
}